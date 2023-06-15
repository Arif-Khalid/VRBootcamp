using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class flashlight : MonoBehaviour
{
    public Light light;
    public InputActionReference flashlightButton;
    //action , cancel , perform
    bool en = true;
    bool isPressed = false;
    float cd = 0f;
    [SerializeField] helmet helm;
    // Start is called before the first frame update
    void Awake(){
        light = GetComponent<Light>();
        flashlightButton.action.started += pushedButton;
        //flashlightButton.action.
    }

    void OnDestroy(){
        flashlightButton.action.started -= pushedButton;
    }
    void Start()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cd > 0f){
            cd -= Time.deltaTime;
        }
    }

    public void pushed(){
        if (cd <= 0f){
            if (en){
                GetComponent<Light>().enabled = false;
                cd = 0.1f;
                en = false;
            }
            else{
                GetComponent<Light>().enabled = true;
                cd = 0.1f;
                en = true;
            }
        }
    }

    public void pushedButton(InputAction.CallbackContext callbackContext){
        if (helm.isInHelmetZone){
            isPressed = !isPressed;
            if (isPressed){
                // ispressed logic
                light.enabled = false;
            }
            else{
                // !ispressed logic
                light.enabled = true;
            }
        }
        
    }
}
