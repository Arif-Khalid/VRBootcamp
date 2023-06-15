using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.XR.PXR;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] GameObject XROrigin;
    [SerializeField] GameObject lHand;
    [SerializeField] GameObject rHand;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject lDirect;
    [SerializeField] GameObject rDirect;
    [SerializeField] BreakableObjectsFactory BOF;
    [SerializeField] GameObject machete;
    GameObject lWeap;
    GameObject rWeap;
    CharacterController XROriginCC;
    int leftArmUp = 1;
    int rightArmUp = 1;

    bool cycle = false;
    bool isRunning = false;
    float timetoStop = 0.5f;

    public bool isSlashingL = false;
    bool lPickUp = false;
    [SerializeField] float timeSlashL = 0.5f;
    bool lCycle = false;

    public bool isSlashingR = false;
    bool rPickUp = false;
    [SerializeField] float timeSlashR = 0.5f;
    bool rCycle = false;
    float score = 0f;
    public InputActionReference resetButton;

    public float speed;
    // Start is called before the first frame update


    public ActionBasedController Lcont;
    public ActionBasedController Rcont;

    void Awake(){
        resetButton.action.started += Reset;
    }
    void Start()
    {
        transform.position = XROrigin.transform.position;
        
        XROriginCC = XROrigin.GetComponent<CharacterController>();
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float height = head.transform.position.y - lHand.transform.position.y;
        
        if (height <= 0.3){
            leftArmUp = 3;
        }
        else if (height  <= 0.4 && !lCycle){
            if (leftArmUp == 3){
                isSlashingL = true;
                timeSlashL = 0.5f;
                lCycle = true;
                if(lPickUp)
                Debug.Log("start left");
            }
            leftArmUp = 2;
           if (!cycle)

            {
                timetoStop = 1f;
                Debug.Log("larmrarm");
                cycle = true;
            }
        }
        else if ( height >= 0.5 ){
            if (leftArmUp < 2 && rightArmUp > 1 && cycle){
                isRunning = true;
                cycle = false;
                timetoStop  = 1f;
                //GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.x , 0, Camera.main.transform.forward.z);
                Debug.Log("hhhh");

            }
            else{
                //isRunning = false;
            }
            leftArmUp = 0;
            isSlashingL = false;
            Debug.Log("end");
        }
        else{
            leftArmUp = 1;
        }
        if (lCycle && leftArmUp != 2){
            lCycle = false;
        }
        height = head.transform.position.y - rHand.transform.position.y;
        float height2 = height;
        if (height <= 0.3){
            rightArmUp = 3;
        }
        if (height <= 0.4){
            if (rightArmUp == 3 && !rCycle){
                if (rPickUp)
                isSlashingR = true;
                timeSlashR = 0.5f;
                rCycle = true;
                Debug.Log("start right");
            }
            rightArmUp = 2;
        }
        else if ( height >= 0.5 ){
            rightArmUp = 0;
            isSlashingR = false;
        }
        else{
            rightArmUp = 1;
        }
        if (rCycle && rightArmUp != 2){
            rCycle = false;
        }
        if (leftArmUp > 2 && rightArmUp > 2 || (leftArmUp  == 0 && rightArmUp == 0)){
            isRunning = false;
        }
        flashlight.transform.position = Camera.main.transform.position + new Vector3(0,0.15f,0);
        flashlight.transform.LookAt(flashlight.transform.position + Camera.main.transform.forward);
        if (isRunning){
            XROriginCC.Move(Camera.main.transform.forward.normalized * Time.deltaTime * speed);
            //Controller Type, Vibration amp(0-1), Frequency 50-500HZ, duration in ms
            //PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, .5f, 10,100);
            //PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .5f, 10,100);
            score += 1f;
        }
        if (timetoStop <= 0f){
            isRunning = false;
        }
        else{
            timetoStop -= Time.deltaTime;
        }
        if (timeSlashL <= 0f){
            isSlashingL = false;
        }
        else{
            timeSlashL -= Time.deltaTime;
        }
        if (timeSlashR <= 0f){
            isSlashingR = false;
        }
        else{
            timeSlashR -= Time.deltaTime;
        }
        if (isSlashingL){
            Debug.Log("slashing left!");
        }
        else if (isSlashingR){
            Debug.Log("slashing right");
        }
        Debug.Log(lWeap);
        if (lWeap != null){
            Lcont.hideControllerModel = true;
            lWeap.transform.position = lHand.transform.position;
        }
        if (rWeap != null){
            rWeap.transform.position = rHand.transform.position;
            Rcont.hideControllerModel = true;
        }
        //XROrigin.transform.position = transform.position;
    }
    public void PickUpWeap(SelectEnterEventArgs ObjSelector){
        Debug.Log(ObjSelector.interactorObject.transform.gameObject);
        if (ObjSelector.interactorObject.transform.gameObject == lDirect){
            lPickUp = true;
            lWeap = ObjSelector.interactableObject.transform.gameObject;
            Lcont.hideControllerModel = true;
        }
        else if (ObjSelector.interactorObject.transform.gameObject == rDirect){
            rPickUp = true;
            rWeap = ObjSelector.interactableObject.transform.gameObject;
            Rcont.hideControllerModel = true;
        }
    }
    public void LetGoWeap(SelectExitEventArgs ObjSelector){
            if (ObjSelector.interactorObject.transform.gameObject == lDirect){
            lPickUp = false;
            isSlashingL = false;
            lWeap = null;
            Lcont.hideControllerModel = false;
        }
           else if (ObjSelector.interactorObject.transform.gameObject == rDirect){
            rPickUp = false;
            isSlashingR = false;   
            rWeap = null;
            Rcont.hideControllerModel = false;
        }
    }
    public void Reset(InputAction.CallbackContext callbackContext){
        PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, 1f, 10,200);
        rPickUp = false;
        lPickUp = false;
        rWeap = null;
        lWeap = null;
        rCycle = false;
        lCycle = false;
        isSlashingL = false;
        isSlashingR = false;
        timeSlashL = 0f;
        timeSlashR = 0f;
        timetoStop = 0f;
        cycle = false;
        isRunning = false;
        leftArmUp = 0;
        rightArmUp = 0;
        XROrigin.transform.position = new Vector3(0,1.5f,0);
        machete.transform.position = new Vector3(1.5f,3f,1.5f);
        BOF.Reset();
        Debug.Log(score);
        score = 0f;
        
    }
}
