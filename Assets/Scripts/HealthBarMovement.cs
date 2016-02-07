using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarMovement : MonoBehaviour {

	// Use this for initialization
	public GameObject Enemy;

	float minValue = 0.0f;
	float maxValue = 1.0f;
	Color minColor = Color.red;
	Color maxColor = Color.green;
	Image img;

	void Awake () {
		img = this.gameObject.GetComponent<Image> ();
	}
	
	public void SetHealthVisual(float healthNormalized){
		this.gameObject.transform.localScale = new Vector3( healthNormalized,
		                                                   this.gameObject.transform.localScale.y,
		                                                   this.gameObject.transform.localScale.z);
	}
	// Update is called once per frame
	void Update () {
		EnemyMovement em = Enemy.GetComponent<EnemyMovement> ();
		img.color = Color.Lerp(minColor,
		                         maxColor,
		                         Mathf.Lerp(minValue,
		           maxValue,
		           transform.localScale.x));
		SetHealthVisual (em.Health/em.MaxHealth);
	}
}
