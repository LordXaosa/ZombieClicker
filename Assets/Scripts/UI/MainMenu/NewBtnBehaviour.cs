using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBtnBehaviour : MonoBehaviour {

	// Use this for initialization
	public Slider loadingBar;
	public Canvas loadSliderCanvas;
	AsyncOperation async;

	void Start () {
	
	}

	public void StartNewGame()
	{
		StartCoroutine (LoadLevel ("MainLevel"));
		loadSliderCanvas.enabled = true;
		SavedData.Data = new SavedData ();
		SavedData.Save ();
	}

	public void Continue()
	{
		if (SavedData.Load ()) {
			StartCoroutine (LoadLevel ("MainLevel"));
			loadSliderCanvas.enabled = true;
		}
	}

	IEnumerator LoadLevel(string level)
	{
		async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone) {
			loadingBar.value = async.progress;
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
