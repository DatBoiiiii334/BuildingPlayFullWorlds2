using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAtributes : MonoBehaviour {

	//Attributen
	public float maxHunger, maxThirst, maxStamina, maxColdness;
	public float hunger, thirst, stamina, coldness;

	//bool's
	private bool running;
	private bool triggeringFirePlace;

	//Sliders
	public Slider hungerBar;
	public Slider thirstBar;

	public Camera fpsCam;

	private float range = 15f;

    public GameObject TheChoppa;

	void Start()
	{
		stamina = maxStamina;

		hungerBar.value = CalculateHunger ();
		thirstBar.value = CalculateThirst ();
	}

	void Update()
	{
//l		hungerBar = hungerVar + hunger;

		if (hunger <= maxHunger) {
			hunger -= 0.2f * Time.deltaTime;
			hungerBar.value = CalculateHunger ();
		} else if (hunger > maxHunger) 
		{
			hunger = 50f;
		}

		if (thirst <= maxThirst) 
		{
			thirst -= 0.5f * Time.deltaTime;
			thirstBar.value = CalculateThirst ();
		} else if (thirst > maxThirst) 
		{
			thirst = 50f;
		}

		if (hunger == 0 || thirst == 0)
		{
			Die ();
		}

		if (Input.GetKey (KeyCode.LeftShift) && stamina >0) 
		{
			stamina -= 10 * Time.deltaTime;
		}
		if (stamina <= 0) 
		{
			print ("Stamina = 0, you can't sprint anymore, wait a bit to sprint again. ");
		}
		
		if (triggeringFirePlace) {
			
			coldness -= 1 * Time.deltaTime;
		} else {
			coldness += 1 * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.E)) 
		{
            Ray ray = fpsCam.ViewportPointToRay(Vector3.one / range);
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
            RaycastHit hit;
			if (Physics.Raycast (fpsCam.transform.position,transform.forward, out hit, range))
            {
                
                Debug.Log (hit.transform.name);

		//		//For picking up food
				if (hit.transform.tag == "Food") 
				{
					hunger = hunger + 25f;
					Destroy (hit.transform.gameObject);
				}

		//		//For picking up Water
				if (hit.transform.tag == "Water") 
				{
                    
					thirst = thirst + 25f;
					Destroy (hit.transform.gameObject);
				}

				//For picking up Medkits
				if (hit.transform.tag == "Med") 
				{
					GlobalHealth.currentHealth = GlobalHealth.currentHealth + 25f;
					Destroy (hit.transform.gameObject);
				}

  //              //For picking up the meguffin
                if (hit.transform.gameObject.name.Contains("Smoke_Grenade"))
                {
                    print("Gottum");
                    transform.Find("Transport_Chopper").gameObject.SetActive(true);
                    //TheChoppa.SetActive(true);
                    Destroy(hit.transform.gameObject);
                }

            }
		}
	}
		


	void OnTriggerEnter (Collider other)
	{
       
            if (other.tag == "Food")
            {

                hunger = hunger + 25f;
                Destroy(other.gameObject);
            }

            if (other.tag == "Water")
            {
                thirst = thirst + 25f;
                Destroy(other.gameObject);
            }

            if (other.tag == "FirePlace")
            {
                triggeringFirePlace = true;
            }

            if (other.tag == "Mag")
            {
                //ammo for the m4
                print("ammo");
                FPS_Shooting.Magz = FPS_Shooting.Magz + 1;
                FPS_Shooting2.Magz = FPS_Shooting.Magz + 1;
                FPS_Shooting3.Magz = FPS_Shooting.Magz + 1;
                Destroy(other.gameObject);
            }

            //For picking up the meguffin
            if (other.tag == "Meg")
            {
                print("Do it");
                TheChoppa.SetActive(true);
                Destroy(other.transform.gameObject);
            }

	}

	void OnTriggerExit (Collider other)
	{

	}


	float CalculateHunger(){
		return hunger / maxHunger;
	}

	float CalculateThirst(){
		return thirst / maxThirst;
	}


	void Die()
	{
		print("You have died of hunger or thirst. ");
	}



}
