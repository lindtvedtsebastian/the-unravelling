using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour {
    public AudioClip menuAudio; // this track loops throughout the all of the MainMenu scene.
    
    public List<AudioClip> dayMusic = new List<AudioClip>();
    public List<AudioClip> nightMusic = new List<AudioClip>();
    private const int sizeOfSoundtrack = 2;
    private AudioSource soundtrackSource;

    private int currentDaySongIndex;
    private int currentNightSongIndex;

    private WorldStateManager stateManager;

    private void Awake() {
        soundtrackSource = gameObject.AddComponent<AudioSource>();
        soundtrackSource.clip = menuAudio;
        soundtrackSource.volume = 0.25f;
        soundtrackSource.loop = true;
        soundtrackSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// OnSceneLoaded is a callback that we use to fetch the statemanager at the correct time.
    /// We need to fetch it WHEN we change the scene because it's only then it comes into existence in the game.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name != "MainGame") return;
        stateManager = GameObject.Find("GameManager").GetComponent<WorldStateManager>();
        StartCoroutine(PlaySoundtrack());
    }
    /*
    IEnumerator playMenuAudio() {
        while (true) {
            currentDaySongIndex++; // play the next song
            if (currentDaySongIndex > sizeOfSoundtrack) { // wrap around the end of the list.
                currentDaySongIndex = 1;
            }
            Music.clip = dayMusic[currentDaySongIndex - 1];
            if (currentScene.name == "MainGame") {
                break;
            }
            Music.Play();
            yield return new WaitForSeconds(Music.clip.length);
        }
        StopCoroutine("playMenuAudio");
        StartCoroutine("PlayDayMusic");
    }
    */
    
    /*
    IEnumerator PlayDayMusic() {
        while (true) {
            currentDaySongIndex++; // play the next song
            if (currentDaySongIndex > sizeOfSoundtrack) { // wrap around the end of the list.
                currentDaySongIndex = 1;
            }
            Music.clip = dayMusic[currentDaySongIndex - 1];
            if (flag && stateManager.IsNight()) { 
                break;
            }
            Music.Play();
            yield return new WaitForSeconds(Music.clip.length);
        }
        StopCoroutine("PlayDayMusic");
        StartCoroutine("PlayNightMusic");
    }
    
    IEnumerator PlayNightMusic() {
        while (true) {
            currentNightSongIndex++;
            if (currentNightSongIndex > sizeOfSoundtrack) {
                currentNightSongIndex = 1;
            }
            Music.clip = nightMusic[currentNightSongIndex - 1];
            if (flag && stateManager.IsDay()) {
                break;
            }
            Music.Play();
            yield return new WaitForSeconds(Music.clip.length);
        }
        StopCoroutine("PlayNightMusic");
        StartCoroutine("PlayDayMusic");
    }*/

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
            if (stateManager.IsDay()) {
                currentDaySongIndex++;
                if (currentDaySongIndex > sizeOfSoundtrack) {
                    currentDaySongIndex = 1;
                }
                soundtrackSource.clip = dayMusic[currentDaySongIndex - 1];
                soundtrackSource.Play();
                yield return new WaitForSeconds(soundtrackSource.clip.length);
            } else if (stateManager.IsNight()) {
                currentNightSongIndex++;
                if (currentNightSongIndex > sizeOfSoundtrack) {
                    currentNightSongIndex = 1;
                }
                soundtrackSource.clip = nightMusic[currentNightSongIndex - 1];
                soundtrackSource.Play();
                yield return new WaitForSeconds(soundtrackSource.clip.length);
            }
        } 
    }
    
}
