using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helmet : MonoBehaviour
{
    public bool isInHelmetZone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Controller"){
            isInHelmetZone = true;
        }
    }
    public void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "Controller"){
            isInHelmetZone = false;
        }
    }
    
}