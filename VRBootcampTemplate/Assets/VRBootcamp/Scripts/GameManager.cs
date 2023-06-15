using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InputActionReference pauseButtonInputActionReference;
    // Player and Monster
    public Transform playerTransform;   // Should instead be the player script whatever it is called
    public Transform playerSpawnPoint;
    public Transform monsterSpawnPoint;
    public MonsterStateMachine monsterStateMachine;
    public PlayerController playerController;


    public Canvas startMenu;
    public Canvas pauseMenu;
    public Canvas settingsMenu;
    public Canvas deathMenu;
    public AudioListener playerAudioListener;
    public Slider audioSlider;
    public ScoreCalculator scoreCalculator;

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
        pauseButtonInputActionReference.action.started += PauseGame;
        AudioManager.instance.Play("crickets");
        playerController.StopMovement();
    }

    public void RestartGame() {
        // Reset player to start point
        // Call reset player function on player script
        deathMenu.enabled = false;
        ResumeGame();
        currentScore = 0;
        monsterStateMachine.reset();
        monsterStateMachine.transform.position = monsterSpawnPoint.transform.position;
        startMenu.enabled = true;
        playerTransform.position = playerSpawnPoint.position;
        playerController.Reset();
        playerController.StopMovement();
        scoreCalculator.ResetCalculation();
    }

    public void StartGame() {
        // Allow player movement
        // start playing some sounds maybe
        // Play gameplay music
        scoreCalculator.StartCoroutine(scoreCalculator.UpdateScore());
        startMenu.enabled = false;
        monsterStateMachine.startChase();
        playerController.ResumeMovement();
    }
    public void PauseGame(InputAction.CallbackContext a) {
        if (deathMenu.enabled) {
            return;
        }
        if (pauseMenu.enabled) {
            Debug.Log("I am trying to resume game");
            ResumeGame();
            return;
        }
        Debug.Log("I have paused the game");
        pauseMenu.enabled = true;
        Time.timeScale = 0;

    }

    public void EnableSettingsMenu() {
        settingsMenu.enabled = true;
        pauseMenu.enabled = false;
        Debug.Log("Enabled settings menu");

    }

    public void DisableSettingsMenu() {
        settingsMenu.enabled = false;
        pauseMenu.enabled = true;
        Debug.Log("Disabled Settings menu");
    }

    public void ResumeGame() {
        if (settingsMenu.enabled) {
            DisableSettingsMenu();
            return;
        }
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        Debug.Log("I have resumed the game");
    }
    public void PlayerDie() {
        Debug.Log("Player has died");
        scoreCalculator.StopAllCoroutines();
        deathMenu.enabled = true;
        playerController.StopMovement();
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

    public void ChangeVolume() {
        AudioListener.volume = audioSlider.value;
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
