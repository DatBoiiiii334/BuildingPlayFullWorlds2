using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

	public GameObject Food;
	public int spawnNum = 3;

	void spawn()
	{
		for (int i = 0; i < spawnNum; i++)
		{
			Vector3 FoodPos = new Vector3 (this.transform.position.x + Random.Range(-1f, 1f),
											this.transform.position.y + Random.Range(0.0f, -2f),
											this.transform.position.z + Random.Range(-1f, 1f));
			Instantiate (Food, FoodPos, Quaternion.identity);
		}
	}


	// Use this for initialization
	void Start () {
		spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
