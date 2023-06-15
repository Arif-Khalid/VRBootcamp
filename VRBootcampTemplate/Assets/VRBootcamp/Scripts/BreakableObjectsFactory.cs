using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectsFactory : MonoBehaviour
{
    [SerializeField] public List<GameObject> Objects;

    public void Reset() {
        for (int objNum = 0; objNum < Objects.Count; objNum++) {
            Objects[objNum].SetActive(true);
        }
    }
}
