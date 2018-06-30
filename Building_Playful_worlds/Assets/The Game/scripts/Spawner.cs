using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	private GameObject enemySpawn;
	public GameObject[] enemyType;
	public int maxEnemies = 32;		//Enemies that are active in the scene
	public int maxEnemiesMem = 128; //Enemies stored in memeory

	List<GameObject> enemies;
	public static int numEnemies;

	private int spawnRange = 100; // Speeks for it self
	private int minSpawnRange = -100; // Speeks for its self
	private int enemySwitch;

	// Use this for initialization
	void Start () 
	{
		enemySwitch = 0;
		numEnemies = 0;
		enemies = new List<GameObject> ();
		for (int i = 0; i < maxEnemiesMem; i++) 
		{
			GameObject obj = (GameObject)Instantiate (GetEnemies ());
			obj.SetActive (false);
			enemies.Add (obj);

			enemySwitch += 1;
			if (enemySwitch > 4) 
			{
				enemySwitch = 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		while (numEnemies < maxEnemies) 
		{
			SpawnEnemies ();
		}
	}

	void SpawnEnemies()
	{
		Vector3 spawnLocation = Random.insideUnitSphere * spawnRange;

		Vector3 finalLocation = new Vector3 (spawnLocation.x, 0, spawnLocation.z);

		if (float.IsInfinity (finalLocation.x) || float.IsInfinity (finalLocation.y) || float.IsInfinity (finalLocation.z)) 
		{
			finalLocation = new Vector3 (5, 0, 20);
			Debug.Log ("fixed infinity");
		}

		if(numEnemies < maxEnemies)
		{
			for (int i = 0; i < enemies.Count; i++)
			{
				if(!enemies[i].activeInHierarchy)
				{
					enemies [i].transform.position = finalLocation;
					enemies [i].transform.rotation = Quaternion.identity;
					enemies [i].SetActive (true);
					numEnemies += 1;
					break;
				}
			}
		}
	}

	GameObject GetEnemies()
	{
		switch (enemySwitch) 
		{
		case 1:
			enemySpawn = enemyType [1];
			break;
		case 2:
			enemySpawn = enemyType [1];
			break;
		case 3:
			enemySpawn = enemyType [1];
			break;
		default:
			enemySpawn = enemyType [0];
			break;
		}
		return enemySpawn;
	}





}
