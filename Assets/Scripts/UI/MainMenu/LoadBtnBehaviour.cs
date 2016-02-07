using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadBtnBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Awake()
	{
		if (!SavedData.Load ()) {
			Button btn = GetComponent<Button> ();
			//btn.enabled = false;
			btn.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
