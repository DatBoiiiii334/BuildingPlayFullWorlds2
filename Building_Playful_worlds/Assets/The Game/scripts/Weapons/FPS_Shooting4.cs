using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class FPS_Shooting4 : MonoBehaviour {

	//public TimeManager timeManager;
	public float damage = 30.0f;
	public float range = 100.0f;
	public float FireRate = 15f;
	public float ImpactForce = 10f;

	public Camera fpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impactEffect;
	private bool isReloading = false;

	public AudioClip GunShot;
	public AudioClip Zooming;

	public int maxAmmo = 10;
	public int  currentAmmo;
	private float reloadTime = 1f;

	public GameObject AmmoDisplay;
	public bool ShowDisplay;


	private float nextTimeToFire = 0f;

	private AudioSource source;
	public Animator animator;

	//public GameObject CaseSpawn;

	public GameObject other1;
	public GameObject other2;
	public GameObject other3;


	//SNIPED
	public GameObject scopeOverlay;
	public GameObject Sniper;
	public GameObject WeaponCamera;
	public Camera mainCamera;
	public float cameraZoom = 15f;
	private bool IsScoped = false;


	//public AnimationClip

	void Start(){
		currentAmmo = maxAmmo;
		source = GetComponent<AudioSource> ();
		ShowDisplay = AmmoDisplay;
		AmmoDisplay.SetActive (true);
		other1.SetActive (true);
		other2.SetActive (true);
		other3.SetActive (true);
		animator.SetBool ("IsScoped", false);
	}

	void OnEnable(){
		isReloading = false;
		animator.SetBool ("Reloading", false);
	}

	// Update is called once per frame
	void Update () 
	{
		AmmoDisplay.GetComponent<Text> ().text = "Sniper:" + currentAmmo;

		if (isReloading)
			return;

		if(currentAmmo <=0)
		{
			StartCoroutine(Reload());
			return;
		}

		if (Input.GetMouseButtonDown (1)) {
			IsScoped = !IsScoped;
			animator.SetBool ("scoped", IsScoped);
			if (IsScoped) {
				StartCoroutine (OnScoped());
			} else {
				OnUnScoped ();
			}
		}

		if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire) 
		{
			source.PlayOneShot (GunShot);
			nextTimeToFire = Time.time + 1 / FireRate;
			Shoot();
		}

		if (Input.GetKeyDown(KeyCode.R)) 
		{
			StartCoroutine(Reload());
			return;
		} 

		if (Input.GetKeyDown(KeyCode.E)) 
		{

			BulletTime();
		} 
	}

	void OnUnScoped(){
		scopeOverlay.SetActive (false);
		WeaponCamera.SetActive (true);
		mainCamera.fieldOfView = 60f;
	}

	IEnumerator OnScoped(){
		yield return new WaitForSeconds (.1f);
		source.PlayOneShot (Zooming);
		yield return new WaitForSeconds (.1f);

		scopeOverlay.SetActive (true);
		WeaponCamera.SetActive (false);
		mainCamera.fieldOfView = cameraZoom;
	}



	IEnumerator Reload()
	{
		isReloading = true;
		Debug.Log("Reloading....");

		animator.SetBool ("Reloading", true);

		yield return new WaitForSeconds (reloadTime -.25f);
		animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (-.25f);

		currentAmmo = maxAmmo;
		isReloading = false;

	}

	void BulletTime(){
		//timeManager.DoSlowMotion();
	}

	void Shoot()
	{

		currentAmmo--;
		//
		RaycastHit hit;
		if(Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
		{

			Debug.Log (hit.transform.name);

			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) 
			{
				target.TakeDamege (damage);
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
