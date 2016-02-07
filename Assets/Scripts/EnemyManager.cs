using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	// Use this for initialization
	public GameObject[] meleeEnemies;
	public GameObject[] rangedEnemies;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;

	public static int EnemyCount = 0;
	//public static int CurrentLevel = 1;
	public static int KillsCount = 0;
	public static int TotalKills = 0;

	GameTimeCounter gtc;
	
	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		gtc = Camera.main.GetComponent<GameTimeCounter> ();
		gtc.StartTimer ();
	}
	
	
	void Spawn ()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		try{
			if(EnemyCount < 10+SavedData.Data.CurrentLevel){
				int random;
				EnemyMovement em;// = enemy.GetComponent<EnemyMovement> ();
				GameObject spawn;
				if(Random.Range(0.0f,1.0f) < 0.1f)
				{
					random = Random.Range(0,rangedEnemies.Length);
					spawn = rangedEnemies[random];
					em = spawn.GetComponent<EnemyMovement> ();
					em.IsRange = true;
					em.Range = 100f;
					em.Damage = Random.Range(0.0f,1.0f)*SavedData.Data.CurrentLevel*3;
					em.MaxHealth = Random.Range (10f,30f)*(float)SavedData.Data.CurrentLevel;
				}
				else{
					random = Random.Range(0,meleeEnemies.Length);
					spawn = meleeEnemies[random];
					em = spawn.GetComponent<EnemyMovement> ();
					em.IsRange = false;
					em.Range = 6f;
					em.Damage = Random.Range(0.0f,1.0f)*SavedData.Data.CurrentLevel;
					em.MaxHealth = Random.Range (10f,30f)*(float)SavedData.Data.CurrentLevel*3;
				}
				if(em.Damage <1)
				{
					em.Damage = 1;
				}
				//Instantiate (spawn, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
				Instantiate (spawn, spawnPoints[spawnPointIndex].position, spawn.transform.rotation);
				EnemyCount++;
			}
		}
		catch(UnityException ex)
		{}
	}
}
