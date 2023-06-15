using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Awake() {
        GameManager.currentScoreUpdated.AddListener(displayScore);
    }
    private void displayScore(float currentScore) {
        scoreText.text = currentScore.ToString("0.00") + " m";
    }
}
