using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

	//Camera
	public Camera fpsCam;

	//distance pick up weapons
	private float range = 4f;

	//weapons
	public GameObject m_Rifle;
	public GameObject ak_Rifle;
	public GameObject UMP;


	//ints
	int weaponOut = 0; //1 = M14, 2 = Ak47

	void Start(){
		
	}

	
	// Update is called once per frame
	void Update () {


		//Picking up weapons
		if (Input.GetKey (KeyCode.E)) 
		{
			RaycastHit hit;
			if (Physics.Raycast (fpsCam.transform.position,transform.forward, out hit, range)) {

                if (hit.transform.tag == "water")
                { print("water"); }

				if (hit.transform.tag == "item") {



					if (hit.transform.gameObject.name.Contains ("M4A1 Sopmod_Fake")) {
						if (weaponOut == 1) {
							Destroy (hit.transform.gameObject);
							Instantiate (m_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 2) {
							Destroy (hit.transform.gameObject);
							Instantiate (ak_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 3) {
							Destroy (hit.transform.gameObject);
							Instantiate (UMP, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if(weaponOut == 0){
							Destroy (hit.transform.gameObject);
						}
						changeWeapon (1);
						
					} else if (hit.transform.gameObject.name.Contains ("Ak-47_Fake")) {
						
						if (weaponOut == 2) {
							Destroy (hit.transform.gameObject);
							Instantiate (ak_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 1) {
							Destroy (hit.transform.gameObject);
							Instantiate (m_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 3) {
							Destroy (hit.transform.gameObject);
							Instantiate (UMP, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if(weaponOut == 0){
							Destroy (hit.transform.gameObject);
						}
						changeWeapon (2);

					} else if (hit.transform.gameObject.name.Contains ("UMP-45_Fake")) {

						if (weaponOut == 3) {
							Destroy (hit.transform.gameObject);
							Instantiate (UMP, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 1) {
							Destroy (hit.transform.gameObject);
							Instantiate (m_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if (weaponOut == 2) {
							Destroy (hit.transform.gameObject);
							Instantiate (ak_Rifle, transform.root.transform.position + new Vector3 (-2, 1, 0), Quaternion.identity);
						} else if(weaponOut == 0){
							Destroy (hit.transform.gameObject);
						}
						changeWeapon (3);
					}
				} 
			}
		}
	}

	void changeWeapon(int weapon)
	{
		if (weapon == 1) 
		{
			weaponOut = 1;
			transform.Find ("M4A1 Sopmod").gameObject.SetActive(true);
			transform.Find ("Ak-47").gameObject.SetActive(false);
			transform.Find ("UMP-45").gameObject.SetActive(false);
		} 

		if (weapon == 2) 
		{
			weaponOut = 2;
			transform.Find ("M4A1 Sopmod").gameObject.SetActive(false);
			transform.Find ("Ak-47").gameObject.SetActive(true);
			transform.Find ("UMP-45").gameObject.SetActive(false);
		} 

		if (weapon == 3) 
		{
			weaponOut = 3;
			transform.Find ("M4A1 Sopmod").gameObject.SetActive(false);
			transform.Find ("Ak-47").gameObject.SetActive(false);
			transform.Find ("UMP-45").gameObject.SetActive(true);
		}

	}

}
