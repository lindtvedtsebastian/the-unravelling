using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour {
    
    // NOTE: Current weakness is that a begun audiotrack has to finish before we can end the coroutine
    public List<AudioClip> dayMusic = new List<AudioClip>();
    public List<AudioClip> nightMusic = new List<AudioClip>();
    private const int sizeOfSoundtrack = 3;

    private AudioSource Music;
    private int currentSongIndex = 0;
    private Scene currentScene;

    private bool flag;
    private WorldStateManager stateManager;

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start() {
        Music = gameObject.AddComponent<AudioSource>();
        Music.clip = dayMusic[0];
        Music.volume = 0.5f;
        Music.Play();
        StartCoroutine("playMenuAudio");
        Debug.Log("Couroutine has started");
    }

    IEnumerator playMenuAudio() {
        while (true) {
            currentSongIndex++; // play the next song
            if (currentSongIndex > sizeOfSoundtrack) { // wrap around the end of the list.
                currentSongIndex = 1;
            }
            Music.clip = dayMusic[currentSongIndex - 1];
            if (currentScene.name == "MainGame") {
                break;
            }
            Music.Play();
            yield return new WaitForSeconds(Music.clip.length);
        }
        StopCoroutine("playMenuAudio");
        StartCoroutine("PlayDayMusic");
    }
    
    IEnumerator PlayDayMusic() {
        while (true) {
            currentSongIndex++; // play the next song
            if (currentSongIndex > sizeOfSoundtrack) { // wrap around the end of the list.
                currentSongIndex = 1;
            }
            Music.clip = dayMusic[currentSongIndex - 1];
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
            currentSongIndex++; // play the next song
            if (currentSongIndex > sizeOfSoundtrack) { // wrap around the end of the list.
                currentSongIndex = 1;
            }
            Music.clip = nightMusic[currentSongIndex - 1];
            if (flag && stateManager.IsDay()) {
                break;
            }
            Music.Play();
            yield return new WaitForSeconds(Music.clip.length);
        }
        StopCoroutine("PlayNightMusic");
        StartCoroutine("PlayDayMusic");
    }

    // Update is called once per frame
    void Update() {
        currentScene = SceneManager.GetActiveScene();
        // NOTE: This is a very hacky solution to grab the gameobject with component we need. This is done because we can't fetch it before we swap scene.
        if (currentScene.name == "MainGame") {
            if (!flag) { // this hack makes it so this is only ran once during the update loop.
                stateManager = GameObject.Find("GameManager").GetComponent<WorldStateManager>();
                flag = true;
            }
        }
    }
}
