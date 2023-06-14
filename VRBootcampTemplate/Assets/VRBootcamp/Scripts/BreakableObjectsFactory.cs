using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectsFactory : MonoBehaviour
{
    [SerializeField] List<GameObject> Objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset(){
        for (int objNum = 0; objNum < Objects.Count; objNum++){
            Objects[objNum].SetActive(true);
        }
    }
}
