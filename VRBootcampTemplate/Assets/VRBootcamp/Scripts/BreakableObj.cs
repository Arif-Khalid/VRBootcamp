using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
public class BreakableObj : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private BreakableObjectsFactory BFO;
    [SerializeField] private ParticleSystem particleSystem;

    // Start is called before the first frame update
    private void Start() {
        BFO.Objects.Add(gameObject);
    }

    private void OnDestroy() {
        BFO.Objects.Remove(gameObject);
    }

    // Update is called once per frame
    private void Update() {

    }
    /*    public void OnTriggerStay(Collider col){
            Debug.Log(Player.isSlashingL);
            if (col.gameObject.tag == "Weapon" && (Player.isSlashingL || Player.isSlashingR )){
                if (Player.isSlashingL){
                    PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.LeftController, .5f, 500,100);
                }
                else{
                    PXR_Input.SendHapticImpulse(PXR_Input.VibrateType.RightController, .5f, 500,100);
                }
                gameObject.SetActive(false);
                Debug.Log("removed"); 
            }
        }*/

    public void Break() {
        gameObject.SetActive(false);
        particleSystem.Play();
        AudioManager.instance.Play("cut");
    }
}
