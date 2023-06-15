using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private GameObject lHand;
    [SerializeField] private GameObject rHand;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject lDirect;
    [SerializeField] private GameObject rDirect;
    [SerializeField] private BreakableObjectsFactory BOF;
    [SerializeField] private GameObject machete;
    [SerializeField] private GameObject lRay;
    [SerializeField] private GameObject rRay;
    [SerializeField] private GameObject weap;
    private GameObject lWeap;
    private GameObject rWeap;
    private CharacterController XROriginCC;
    private int leftArmUp = 1;
    private int rightArmUp = 1;
    private bool cycle = false;
    private bool isRunning = false;
    private float timetoStop = 0.5f;

    public bool isSlashingL = false;
    private bool lPickUp = false;
    [SerializeField] public float timeSlashL = 0.5f;
    private bool lCycle = false;

    public bool isSlashingR = false;
    private bool rPickUp = false;
    [SerializeField] private float timeSlashR = 0.5f;
    private bool rCycle = false;
    private float score = 0f;
    public InputActionReference rayButton;
    public float speed;
    // Start is called before the first frame update


    public ActionBasedController Lcont;
    public ActionBasedController Rcont;
    private float maxHeightL;
    private float prevHeightL;
    private float heightTimeL;
    private float maxHeightR;
    private float prevHeightR;
    private float heightTimeR;

    private void Awake() {
        rayButton.action.started += rayDebug;
    }

    private void OnDestroy() {
        rayButton.action.started -= rayDebug;
    }

    private void Start() {
        transform.position = XROrigin.transform.position;
        Physics.IgnoreCollision(XROrigin.GetComponent<CharacterController>(), weap.GetComponent<BoxCollider>());
        XROriginCC = XROrigin.GetComponent<CharacterController>();
        lRay.SetActive(false);
        rRay.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        float height = head.transform.position.y - lHand.transform.position.y;
        if (height <= 0.3) {
            leftArmUp = 3;
        }
        else if (height <= 0.4 && !lCycle) {
            if (leftArmUp == 3) {
                //isSlashingL = true;
                //timeSlashL = 0.5f;
                //lCycle = true;
                //if(lPickUp)
            }
            leftArmUp = 2;
            if (!cycle) {
                timetoStop = 1f;
                cycle = true;
            }
        }
        else if (height >= 0.5) {
            if (leftArmUp < 2 && rightArmUp > 1 && cycle) {
                isRunning = true;
                cycle = false;
                timetoStop = 1f;
                //GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward.x , 0, Camera.main.transform.forward.z);

            }
            else {
                //isRunning = false;
            }
            leftArmUp = 0;
            //isSlashingL = false;
        }
        else {
            leftArmUp = 1;
        }
        if (lCycle && leftArmUp != 2) {
            lCycle = false;
        }
        height = head.transform.position.y - rHand.transform.position.y;
        float height2 = height;
        if (height <= 0.3) {
            rightArmUp = 3;
        }
        else if (height <= 0.4) {
            if (rightArmUp == 3 && !rCycle) {
                //if (rPickUp)
                //isSlashingR = true;
                //timeSlashR = 0.5f;
                //rCycle = true;
            }
            rightArmUp = 2;
        }
        else if (height >= 5) {
            rightArmUp = 0;
            // isSlashingR = false;
        }
        else {
            rightArmUp = 1;
        }
        if (rCycle && rightArmUp != 2) {
            // rCycle = false;
        }
        if (leftArmUp > 2 && rightArmUp > 2 || (leftArmUp == 0 && rightArmUp == 0)) {
            isRunning = false;
        }
        flashlight.transform.position = Camera.main.transform.position + new Vector3(0, 0.15f, 0);
        flashlight.transform.LookAt(flashlight.transform.position + Camera.main.transform.forward);
        if (isRunning) {
            XROriginCC.Move(new Vector3((Camera.main.transform.forward.normalized * Time.deltaTime * speed).x, -4.9f, (Camera.main.transform.forward.normalized * Time.deltaTime * speed).z));
            //Controller Type, Vibration amp(0-1), Frequency 50-500HZ, duration in ms
            //PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, .5f, 10,100);
            //PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .5f, 10,100);
            score += 1f;
        }
        if (timetoStop <= 0f) {
            isRunning = false;
        }
        else {
            timetoStop -= Time.deltaTime;
        }
        if (timeSlashL <= 0f) {
            isSlashingL = false;
        }
        else {
            PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, .5f, 10, 100);
            timeSlashL -= Time.deltaTime;
        }
        if (timeSlashR <= 0f) {
            isSlashingR = false;
        }
        else {
            PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .5f, 10, 100);
            timeSlashR -= Time.deltaTime;
        }
        if (isSlashingL) {
            Debug.Log("slashing left!");
        }
        else if (isSlashingR) {
            Debug.Log("slashing right");
        }

        if (lWeap != null) {
            Lcont.hideControllerModel = true;
            lWeap.transform.position = lHand.transform.position;
            if (prevHeightL < lHand.transform.position.y) {
                maxHeightL = lHand.transform.position.y;
                heightTimeL = 0f;
            }
            else {
                if (heightTimeL <= 0f) {
                    heightTimeL = 0.25f;
                    maxHeightL = lHand.transform.position.y;
                    isSlashingL = false;
                    timeSlashL = 0f;
                }
                else {
                    heightTimeL -= Time.deltaTime;
                }
            }

            if (heightTimeL > 0f && (maxHeightL - lHand.transform.position.y) / (0.25 - heightTimeL) > 2 && !isSlashingL) {
                isSlashingL = true;
                timeSlashL = 1.0f;
            }
        }
        if (rWeap != null) {
            Rcont.hideControllerModel = true;
            rWeap.transform.position = rHand.transform.position;
            if (prevHeightR < rHand.transform.position.y) {
                maxHeightR = rHand.transform.position.y;
                heightTimeR = 0f;
            }
            else {
                if (heightTimeR <= 0f) {
                    heightTimeR = 0.25f;
                    maxHeightR = rHand.transform.position.y;
                    isSlashingR = false;
                    timeSlashR = 0f;
                }
                else {
                    heightTimeR -= Time.deltaTime;
                }
            }

            if (heightTimeR > 0f && (maxHeightR - rHand.transform.position.y) / (0.25 - heightTimeR) > 2 && !isSlashingR) {
                isSlashingR = true;
                timeSlashR = 1.0f;
            }
        }
        if (rWeap != null) {
            rWeap.transform.position = rHand.transform.position;
            Rcont.hideControllerModel = true;
        }
        //XROrigin.transform.position = transform.position;
        prevHeightL = lHand.transform.position.y;
        prevHeightR = rHand.transform.position.y;
        if (lWeap != weap && rWeap != weap) {
            weap.transform.position = Camera.main.transform.position - new Vector3(0.1f, 0.5f, 0);
            weap.transform.eulerAngles = new Vector3(90f, 180f, 0);
        }
    }
    public void PickUpWeap(SelectEnterEventArgs ObjSelector) {
        if (ObjSelector.interactorObject.transform.gameObject == lDirect) {
            lPickUp = true;
            lWeap = ObjSelector.interactableObject.transform.gameObject;
            Lcont.hideControllerModel = true;
        }
        else if (ObjSelector.interactorObject.transform.gameObject == rDirect) {
            rPickUp = true;
            rWeap = ObjSelector.interactableObject.transform.gameObject;
            Rcont.hideControllerModel = true;
        }
    }
    public void LetGoWeap(SelectExitEventArgs ObjSelector) {
        if (ObjSelector.interactorObject.transform.gameObject == lDirect) {
            lPickUp = false;
            //isSlashingL = false;
            lWeap = null;
            Lcont.hideControllerModel = false;
        }
        else if (ObjSelector.interactorObject.transform.gameObject == rDirect) {
            rPickUp = false;
            isSlashingR = false;
            rWeap = null;
            Rcont.hideControllerModel = false;
        }
    }
    public void Reset() {
        PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, 1f, 10, 200);
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
        weap.transform.position = Camera.main.transform.position - new Vector3(0.1f, 0.5f, 0);
        weap.transform.eulerAngles = new Vector3(90f, 180f, 0);
        BOF.Reset();
        Debug.Log(score);
        score = 0f;
    }

    public void rayActive() {
        lRay.SetActive(true);
        rRay.SetActive(true);
    }
    public void rayInactive() {
        lRay.SetActive(true);
        rRay.SetActive(true);
    }
    public void rayDebug(InputAction.CallbackContext callbackContext) {
        if (lRay.activeSelf) {
            lRay.SetActive(false);
            rRay.SetActive(false);
        }
        else {
            lRay.SetActive(true);
            rRay.SetActive(true);
        }
    }
}
