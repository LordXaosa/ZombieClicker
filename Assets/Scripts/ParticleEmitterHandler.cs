using UnityEngine;
using System.Collections;

public class ParticleEmitterHandler : MonoBehaviour {

	// Use this for initialization
	ParticleSystem ps;
	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!ps.IsAlive ()) {
			Destroy(gameObject);
		}
	}
}
