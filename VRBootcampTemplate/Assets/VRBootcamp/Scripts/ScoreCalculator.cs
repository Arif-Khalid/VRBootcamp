using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public GameManager gameManager;
    private Vector3 previousPosition;

    public void ResetCalculation() {
        previousPosition = transform.position;
    }

    public IEnumerator UpdateScore() {
        while (true) {
            Debug.Log(previousPosition);
            Debug.Log(transform.position);
            Debug.Log(Vector3.Distance(previousPosition, transform.position));
            gameManager.addScore(Vector3.Distance(previousPosition, transform.position));
            previousPosition = transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
