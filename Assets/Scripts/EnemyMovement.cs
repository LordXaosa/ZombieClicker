using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	GameObject playerBase;

	public float speed = 5f;
	public float MaxHealth = 100f;
	public GameObject[] Projectiles;
	float health;
	public float Health
	{ get { return health; } }
	public bool IsRange = false;
	public float Range = 6f;
	public float Damage = 1f;
	bool touchedBase = false;
	public float MaxAttackTime = 1f;
	float currentAttackTime = 0f;
	float standupAnim = 2.5f;
	Animator anim;
	float lifetime = 4f;
	public bool IsDead = false;
	float pauseMoving = 0f;
	
	void Awake ()
	{
		playerBase = GameObject.FindGameObjectWithTag ("Finish");
		health = MaxHealth;
		anim = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate ()
	{
		if (standupAnim > 0) {
			standupAnim -= Time.deltaTime;
			return;
		} else {
			anim.SetBool("IsWalking", true);
		}
		if (lifetime < 0) {
			Destroy (gameObject);
			return;
		} else {
			if(Health<=0){
				lifetime-=Time.deltaTime;
			}
		}
		if (IsDead) {
			if(lifetime< 2f){
				transform.position += new Vector3 (0f, -0.05f, 0f);
			}
			return;
		}
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 5f)) {
			if (hit.transform.gameObject.CompareTag ("Enemy")) {
				EnemyMovement emov = hit.transform.gameObject.GetComponent<EnemyMovement>();
				if(!emov.IsDead){
					anim.SetBool ("IsWalking", false);
				}
				else
				{
					if (!touchedBase) {
						if(Health>0){
							transform.position += new Vector3 (-0.1f, 0f, 0f);
							anim.SetBool ("IsWalking", true);
						}
					}
				}
			}
		} else if (!touchedBase) {
			if(Health>0){
				if(pauseMoving <= 0){
					transform.position += new Vector3 (-0.1f, 0f, 0f);
					anim.SetBool ("IsWalking", true);
					pauseMoving = 0f;
				}
				else
				{
					pauseMoving -= Time.deltaTime;
				}
			}
		}
		if (!IsRange && Physics.Raycast (transform.position, transform.forward, out hit, Range)) {
			if (hit.transform.gameObject.CompareTag ("Finish")) {
				touchedBase = true;
				anim.SetBool ("IsWalking", false);
				if(currentAttackTime >= MaxAttackTime){
					BaseHealth.Health-=(int)Damage;
					/*if(IsRange){
						int random = Random.Range (0,Projectiles.Length);
						RangeProjectile pr = Projectiles[random].GetComponent<RangeProjectile>();
						pr.Damage = 10f;
						Instantiate (Projectiles[random], transform.position, transform.rotation);
					}*/
					anim.SetBool("IsAttacking", true);
					currentAttackTime = 0f;
				}
				else{
					currentAttackTime+=Time.deltaTime;
					anim.SetBool("IsAttacking", false);
				}
			}
		}
		else if (IsRange && Physics.Raycast (transform.position, transform.forward, out hit, Range, (1<<9))) {
			RaycastHit hit2 = hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit, 6f, (1<<9))) {
				if (hit.transform.gameObject.CompareTag ("Finish")) {
					touchedBase = true;
					anim.SetBool ("IsWalking", false);
					anim.SetBool("IsMelee", true);
					if(currentAttackTime >= MaxAttackTime){
						BaseHealth.Health-=(int)Damage;
						/*int random = Random.Range (0,Projectiles.Length);
						RangeProjectile pr = Projectiles[random].GetComponent<RangeProjectile>();
						pr.Damage = Damage;
						Instantiate (Projectiles[random], transform.position+ new Vector3(0f,5f,0f), transform.rotation);
						*/
						currentAttackTime = 0f;
						anim.SetBool("IsAttacking", true);
						pauseMoving = 1.1f;
					}
					else{
						currentAttackTime+=Time.deltaTime;
						anim.SetBool("IsAttacking", false);
					}
				}
			}
			else
			{
				if (hit2.transform.gameObject.CompareTag ("Finish")) {
					if(currentAttackTime >= MaxAttackTime){
						//BaseHealth.Health-=(int)Damage;
						int random = Random.Range (0,Projectiles.Length);
						RangeProjectile pr = Projectiles[random].GetComponent<RangeProjectile>();
						pr.Damage = Damage;
						Instantiate (Projectiles[random], transform.position+ new Vector3(0f,5f,0f), transform.rotation);
						currentAttackTime = 0f;
						anim.SetBool("IsAttacking", true);
						pauseMoving = 1.1f;
					}
					else{
						currentAttackTime+=Time.deltaTime;
						anim.SetBool("IsAttacking", false);
					}
				}
			}
		} 

	}

	public void DealDamage (float dmg)
	{
		if (!IsDead) {
			health -= dmg;
			SavedData.Data.TotalClicks++;
		}
		if (health <= 0 && !IsDead) {
			//Destroy (this.gameObject);
			anim.SetTrigger("IsDead");
			IsDead = true;
			health = 0;
			EnemyManager.EnemyCount--;
			if(++EnemyManager.KillsCount > 10)
			{
				SavedData.Data.CurrentLevel++;
				EnemyManager.KillsCount = 0;
				SavedData.Save ();
			}
			SavedData.Data.TotalKills++;
		} 
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Finish")) {
			touchedBase = true;
			anim.SetBool("IsMelee", true);
		}
	}
}
