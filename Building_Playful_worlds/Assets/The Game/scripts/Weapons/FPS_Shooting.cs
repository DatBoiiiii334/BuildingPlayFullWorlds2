using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class FPS_Shooting : MonoBehaviour {

	//public TimeManager timeManager;
	public float damage = 30.0f;
	public float range = 100.0f;
	public float FireRate = 15f;
	public float ImpactForce = 10f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	private bool isReloading = false;
	private bool isAiming = false;

	public AudioClip GunShot;
	public AudioClip Reloading;

	public int maxAmmo = 10;
	public int  currentAmmo;
	private float reloadTime = 1f;
	public static int Magz;

	public GameObject AmmoDisplay;
	public GameObject MagDisplay;
	public bool ShowDisplay;


	private float nextTimeToFire = 0f;

	private AudioSource source;
	public Animator animator;
	//private float Timer = 0f;

	public GameObject CrossHair;


	//Testting
	private Vector3 originalPostion;
	public Vector3 aimPosition;
	public float aodSpeed = 8f;


	void Start(){

	//testing
		Magz = 1;
		originalPostion = transform.localPosition;
		currentAmmo = maxAmmo;
		source = GetComponent<AudioSource> ();
		ShowDisplay = AmmoDisplay;
		AmmoDisplay.SetActive (true);

		animator.SetBool ("AimRifle", false);

	}

	void OnEnable(){
		isReloading = false;
		animator.SetBool ("Reloading", false);
	}

	// Update is called once per frame
	void Update () 
	{

		//AimDownSights ();

		//Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		//Debug.DrawRay(transform.position, forward, Color.green);

		AmmoDisplay.GetComponent<Text> ().text = "M4A: " + currentAmmo;
		MagDisplay.GetComponent<Text> ().text = "Mags: " + Magz;

		if (isReloading)
			return;

		if(currentAmmo <= 0)
		{
			currentAmmo = 0;
		}

		if (Magz <= 0) 
		{
			Magz = 0;
		}

		//if (Magz <= -1) 
		//{
		//	Magz = 0;
		//	currentAmmo = 0;
		//	AmmoDisplay.GetComponent<Text> ().text = "M4A: " + "--";
		//	return;
		//}

		if (Input.GetMouseButton (0) && Time.time >= nextTimeToFire) {
			nextTimeToFire = Time.time + 1 / FireRate;
			if (currentAmmo >= 1) {
				Shoot ();
				source.PlayOneShot (GunShot);
				animator.SetBool ("RifleShoot", true);
			}
		}else {
				animator.SetBool ("RifleShoot", false);
			}



		if (Input.GetMouseButton (1) && !isReloading) {
			transform.localPosition = Vector3.Lerp (transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
			animator.SetBool ("AimRifle", true);
			CrossHair.SetActive (false);
		} else {
			transform.localPosition = Vector3.Lerp (transform.localPosition, originalPostion, Time.deltaTime * aodSpeed);
			animator.SetBool ("AimRifle", false);
			CrossHair.SetActive (true);
		}


		if (Magz >= 1) {
			if (Input.GetKeyDown (KeyCode.R)) {
				StartCoroutine (Reload ());
				Magz = Magz - 1;
				return;
			}
		}



	}//End update


	//private void AimDownSights()
	//{
	//	if (Input.GetMouseButton (1) && !isReloading) {
	//		transform.localPosition = Vector3.Lerp (transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
	//		animator.SetBool ("AimRifle", true);
	//		CrossHair.SetActive (false);
	//	} else {
	//		transform.localPosition = Vector3.Lerp (transform.localPosition, originalPostion, Time.deltaTime * aodSpeed);
	//		animator.SetBool ("AimRifle", false);
	//		CrossHair.SetActive (true);
	//	}
	//}


	//void Aiming(){
	//	animator.SetBool ("AimRifle", true);

	//}
		
	IEnumerator Reload()
	{
			isReloading = true;
			Debug.Log("Reloading....");
			source.PlayOneShot (Reloading);
			animator.SetBool ("Reloading", true);

			yield return new WaitForSeconds (reloadTime -.25f);
			animator.SetBool ("Reloading", false);
			yield return new WaitForSeconds (-.25f);

			currentAmmo = maxAmmo;
			isReloading = false;
	}
		

	void Shoot()
	{
		//Debug.Log ("shot");
		currentAmmo--;
		//
		RaycastHit hit;
		if(Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{
			//Give Damage to "the target" in the Target script
			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) 
			{
				target.TakeDamege (damage);
			}

			//Give Damage to the "Enemey" in the Chase script
			Chase Enemy = hit.transform.GetComponent<Chase> ();
			if (Enemy != null) 
			{
				Enemy.TakeDamege (damage);
			}

			if (hit.rigidbody != null) 
			{
				hit.rigidbody.AddForce (-hit.normal * ImpactForce);
			}

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.2f);
		}
	}
}

