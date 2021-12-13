using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader: MonoBehaviour {
	public GameObject progressBarObject;
	public Slider progressBar;

	public void loadScene(string sceneName) {
		StartCoroutine(loadAsync(sceneName));
	}

	IEnumerator loadAsync(string sceneName) {
		AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneName);

		progressBarObject.SetActive(true);

		while (!loadScene.isDone) {
			float progress = Mathf.Clamp01(loadScene.progress / .9f);
			progressBar.value = progress;
			yield return null;
		}
	}
}