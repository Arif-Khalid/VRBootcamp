using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float lerpSpeed = 1;
    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpSpeed);
        transform.forward = transform.position - player.position;
    }
}
