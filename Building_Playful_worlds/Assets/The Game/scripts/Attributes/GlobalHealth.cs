using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GlobalHealth : MonoBehaviour {

	public static float currentHealth;
	public float maxHealth;

	//public static int playerHealth = 100;
	//public int internalHealth;
	public Slider HealthDisplay;

	//public Slider Health;

	void Start(){
		currentHealth = 100;
		maxHealth = 100;
		AudioListener.volume = 0.1f;
	}

	float CalculateHealth(){
		return currentHealth / maxHealth;
	}


	void Update(){
		//internalHealth = currentHealth;
		HealthDisplay.value = CalculateHealth ();

		if (Input.GetKeyDown (KeyCode.K)) 
		{
			currentHealth = currentHealth - 1;
		}
		//HealthDisplay.GetComponent<Text> ().text = "Health:" + playerHealth;
		//Health.value = InternalHealth;
		if (currentHealth == 0) 
		{
			SceneManager.LoadScene (1);
		}
	}
}
