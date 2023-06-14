using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundManager : MonoBehaviour
{
    public float timeBetweenSounds;
    public AudioSource audioSource;
    public AudioSource punch1AudioSource;
    public AudioSource punch2AudioSource;
    public AudioSource step1AudioSource;
    public AudioSource step2AudioSource;
    public List<MonsterSound> monsterSounds = new List<MonsterSound>();
    private List<MonsterSound> tempMonsterSounds = new List<MonsterSound>();
    private bool soundStarted = false;

    public void StartSounds() {
        if (soundStarted) {
            return;
        }
        soundStarted = true;
        StartCoroutine(LoopSounds());
    }

    public void EndSounds() {
        soundStarted = false;
        StopAllCoroutines();
    }

    private IEnumerator LoopSounds() {
        while (true) {
            if (tempMonsterSounds.Count == 0) {
                tempMonsterSounds = new List<MonsterSound>(monsterSounds);
            }
            int indexToUse = Random.Range(0, tempMonsterSounds.Count);
            MonsterSound soundToUse = tempMonsterSounds[indexToUse];
            tempMonsterSounds[indexToUse] = tempMonsterSounds[tempMonsterSounds.Count - 1];
            tempMonsterSounds.RemoveAt(tempMonsterSounds.Count - 1);
            audioSource.clip = soundToUse.audioClip;
            audioSource.volume = soundToUse.volume;
            audioSource.pitch = soundToUse.pitch;
            audioSource.Play();
            yield return new WaitForSeconds(timeBetweenSounds);
        }
    }

    public void PlayPunchSound1() {
        punch1AudioSource.Play();
    }

    public void PlayPunchSound2() {
        punch2AudioSource.Play();
    }

    public void PlayStepSound1() {
        step1AudioSource.Play();
    }

    public void PlayStepSound2() {
        step2AudioSource.Play();
    }

    [System.Serializable]
    public class MonsterSound
    {
        public AudioClip audioClip;
        public float pitch;
        public float volume;
    }
}
