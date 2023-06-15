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

    // Variables
    [SerializeField] private float timeBetweenForestSounds;
    private float highScore = 0;
    private float currentScore = 0;

    // Events
    public static UnityEvent<float> currentScoreUpdated = new UnityEvent<float>();

    public List<String> forestSoundTags = new List<String>();
    private List<String> tempForestSoundTags = new List<String>();

    private void Awake() {
        highScore = PlayerPrefs.GetFloat("highScore");
        StartCoroutine(PlayForestSounds());
    }

    private void Start() {
        // Play menu music
        AudioManager.instance.Play("crickets");
    }

    public void RestartGame() {
        // Reset player to start point
        // Call reset player function on player script
        currentScore = 0;
        monsterStateMachine.changeState(monsterStateMachine.monsterIdleState);
        monsterStateMachine.transform.position = monsterSpawnPoint.transform.position;
    }

    public void StartGame() {
        // Allow player movement
        // start playing some sounds maybe
        // Play gameplay music
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

    private IEnumerator PlayForestSounds() {
        while (true) {
            if (tempForestSoundTags.Count == 0) {
                tempForestSoundTags = new List<string>(forestSoundTags);
            }
            int indexToUse = UnityEngine.Random.Range(0, tempForestSoundTags.Count);
            string tagToPlay = tempForestSoundTags[indexToUse];
            tempForestSoundTags[indexToUse] = tempForestSoundTags[tempForestSoundTags.Count - 1];
            tempForestSoundTags.RemoveAt(tempForestSoundTags.Count - 1);
            AudioManager.instance.Play(tagToPlay);
            yield return new WaitForSeconds(timeBetweenForestSounds);
        }
    }
}
