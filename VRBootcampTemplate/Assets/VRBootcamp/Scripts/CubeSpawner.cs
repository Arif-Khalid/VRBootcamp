using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate()
    {
        objectPooler.SpawnFromPooler("Cube", transform.position, Quaternion.identity);
    }
}
