using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Player and Monster
    public Transform playerTransform;   // Should instead be the player script whatever it is called
    public Transform playerSpawnPoint;
    public Transform monsterSpawnPoint;
    public MonsterStateMachine monsterStateMachine;

    // Scores
    private float highScore = 0;
    private float currentScore = 0;

    // Events
    public static UnityEvent<float> currentScoreUpdated = new UnityEvent<float>();

    private void Awake() {
        highScore = PlayerPrefs.GetFloat("highScore");
    }

    public void StartGame() {
        // Reset player to start point
        // call reset player function on player script
        currentScore = 0;
        monsterStateMachine.changeState(monsterStateMachine.monsterIdleState);
        monsterStateMachine.transform.position = monsterSpawnPoint.transform.position;
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    public void EndGame() {
        SaveData();
        Application.Quit();
    }

    public void SaveData() {
        PlayerPrefs.SetFloat("highScore", highScore);
    }

    public void addScore(float amountToAdd) {
        currentScore += amountToAdd;
        if (currentScore > highScore) {
            highScore = currentScore;
        }
        currentScoreUpdated.Invoke(currentScore);
    }

    public void OnDestroy() {
        SaveData();
    }
}
