using UnityEngine;
using System.Collections;

public class RangeProjectile : MonoBehaviour {

	// Use this for initialization
	public float Damage = 1f;

	float lifetime = 2f;
	bool damaged = false;
	Rigidbody rgb;
	bool trigger = false;
	ParticleSystem ps;
	void Start () {
		rgb = GetComponent<Rigidbody> ();
		ps = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!trigger) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 5000f, (1<<9))) {
				if (hit.transform.gameObject.CompareTag ("Finish")) {
					float distance = Vector3.Distance (transform.position, hit.point);
					float speed = 50f;
					float sin2a = distance/(Mathf.Pow(speed,2));
					float a = 0.5f*Mathf.Asin(sin2a)*1.1f;
					Vector3 vel = new Vector3 (-(Mathf.Cos (a) * speed), Mathf.Sin (a) * 10f*speed, 0f);
					rgb.velocity = vel;
				}
			}
			trigger = true;
		}
		if (damaged) {
			lifetime-=Time.deltaTime;
			if(lifetime <=0)
			{
				Destroy(gameObject);
			}
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Finish") && !damaged) {
			BaseHealth.Health-=(int)Damage;
			rgb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
			//Destroy(gameObject);
			rgb.isKinematic = true;
			rgb.velocity = Vector3.zero;
			rgb.angularVelocity = Vector3.zero;
			rgb.useGravity = false;
			damaged = true;
			ps.enableEmission = false;
		}
	}

}
