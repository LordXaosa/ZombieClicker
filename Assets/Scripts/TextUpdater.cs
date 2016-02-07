using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextUpdater : MonoBehaviour {

	// Use this for initialization
	Text text;
	void Start () {
		text = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (text != null) {
			text.text = string.Format (@"Level:{0}
Total kills:{1}
Enemy count:{2}
Base health:{3}
Total clicks:{4}", SavedData.Data.CurrentLevel, SavedData.Data.TotalKills, EnemyManager.EnemyCount, BaseHealth.Health, SavedData.Data.TotalClicks);
		}
	}
}
