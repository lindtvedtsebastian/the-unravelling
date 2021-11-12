using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    private static AudioManager _instance;

    public AudioClip menuAudio; // this track loops throughout the all of the MainMenu scene.
    public AudioMixerGroup MusicSoundGroup;
    public List<AudioClip> dayMusic = new List<AudioClip>();
    public List<AudioClip> nightMusic = new List<AudioClip>();
   
    private const int sizeOfDaySoundtrack = 7;
    private const int sizeOfNightSoundtrack = 2;
    private AudioSource soundtrackSource;
    
    private int currentDaySongIndex;
    private int currentNightSongIndex;

    private CycleState previousState;

    private WorldStateManager stateManager;
    
    private void Awake() {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
        
        soundtrackSource = gameObject.AddComponent<AudioSource>();
        soundtrackSource.outputAudioMixerGroup = MusicSoundGroup;
        soundtrackSource.clip = menuAudio;
        //soundtrackSource.volume = 0.32f;
        soundtrackSource.loop = true;
        soundtrackSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
    }
    
    /// <summary>
    /// OnSceneLoaded is a callback that we use to fetch the statemanager at the correct time.
    /// We need to fetch it WHEN we change the scene because it's only then it comes into existence in the game.
    /// </summary>
    /// <param name="scene">Name of the scene we are going to when we load</param>
    /// <param name="mode">The blend mode of the new scene</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "MainGame") {
            stateManager = GameObject.Find("GameManager").GetComponent<WorldStateManager>();
            StartCoroutine(PlaySoundtrack());
        }
    }

    /// <summary>
    /// PlaySoundtrack() is the "controller" that plays the track's one by one. It plays the list of music based on
    /// the current state of the game in terms of day and night.
    /// NOTE: Drawback is that we need to make it so the last day song and night song exactly fall on the change between state.
    /// Otherwise it could bleed into the wrong state before it realises and changes.
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySoundtrack() {
        soundtrackSource.loop = false; // since we now play the soundtrack track by track, we don't want looping.
        while (true) {
			if (previousState != stateManager.worldState.stateOfDay)
                soundtrackSource.Stop();
			
            if (stateManager.IsDay() && !soundtrackSource.isPlaying) {
                currentDaySongIndex++;
                if (currentDaySongIndex > sizeOfDaySoundtrack) {
                    currentDaySongIndex = 1;
                }
                soundtrackSource.clip = dayMusic[currentDaySongIndex - 1];
                previousState = CycleState.DAY;
                soundtrackSource.Play();
            } else if (stateManager.IsNight() && !soundtrackSource.isPlaying) {
                currentNightSongIndex++;
                if (currentNightSongIndex > sizeOfNightSoundtrack) {
                    currentNightSongIndex = 1;
                }
                soundtrackSource.clip = nightMusic[currentNightSongIndex - 1];
                previousState = CycleState.NIGHT;
                soundtrackSource.Play();
            }
            yield return new WaitForSeconds(5f);
        } 
    }
}
