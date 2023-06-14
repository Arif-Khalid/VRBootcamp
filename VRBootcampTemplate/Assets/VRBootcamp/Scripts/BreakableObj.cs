using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.PXR;
public class BreakableObj : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Weapon" && (Player.isSlashingL || Player.isSlashingR)){
            if (Player.isSlashingL){
                PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, .5f, 500,100);
            }
            else{
                PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .5f, 500,100);
            }
            gameObject.SetActive(false);
            Debug.Log("removed"); 
        }
    }
}
