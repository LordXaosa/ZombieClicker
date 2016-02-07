using UnityEngine;
using System.Collections;

public class GameTimeCounter : MonoBehaviour {

	// Use this for initialization
	bool isStarted = false;
	float time = 0f;

	public float Timer{get{return time;}}

	void Start () {
	
	}

	public void StartTimer(){
		if(!isStarted)
		{
			isStarted = true;
			time = 0f;
		}
	}

	public void StopTimer()
	{
		isStarted = false;
	}

	// Update is called once per frame
	void Update () {
		if (isStarted) {
			time += Time.deltaTime;
		}
	}
}
