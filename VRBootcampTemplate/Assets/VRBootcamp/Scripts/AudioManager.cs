using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public Sound[] sounds;
    private Dictionary<string, AudioSource> soundDict = new Dictionary<string, AudioSource>();

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.reverbZoneMix = 0.2f;
            soundDict.Add(s.name, s.source);
        }
    }

    public void Play(string name) {
        soundDict[name].Play();
    }
}
