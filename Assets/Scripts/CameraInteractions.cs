using UnityEngine;
using System.Collections;

public class CameraInteractions : MonoBehaviour {
	// Use this for initialization
	bool leftclick = false;
	public GameObject HitPrefab;
	void Awake ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (!leftclick) {
				if (SkillActivate.selectedSkill >= 0) {
					leftclick = true;
					return;
				}
				if (Physics.Raycast (ray, out hit))
				{
					EnemyMovement em = hit.transform.gameObject.GetComponent<EnemyMovement> ();
					var mousePos = Input.mousePosition;
					mousePos.z = 45.0f;       // we want 2m away from the camera position
					
					var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
					
					//var myObject= Instantiate(testObject, objectPos, Quaternion.identity);
					//Instantiate(HitPrefab, hit.point + new Vector3(0,3f,0), Quaternion.identity);

					var go = Instantiate(HitPrefab, objectPos, Quaternion.identity);
					Destroy(go,1);
					if (em != null)
					{
						em.DealDamage (10f);
						leftclick = true;
					}
				}
				leftclick = true;
			}
		}
		else {
			leftclick = false;
		}
	}
}
