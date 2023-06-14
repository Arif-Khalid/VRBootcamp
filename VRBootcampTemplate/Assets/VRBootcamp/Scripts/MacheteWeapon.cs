using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteWeapon : MonoBehaviour
{
    [SerializeField] GameObject handle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handle.transform.position;
        transform.rotation = handle.transform.rotation;
    }
}
