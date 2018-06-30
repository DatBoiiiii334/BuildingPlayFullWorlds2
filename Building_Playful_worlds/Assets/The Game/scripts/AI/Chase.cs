using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//public enum State { Idle, Aggro, Patrol}

public class Chase : MonoBehaviour {

    public enum State { Idle, Aggro, Patrol }
    public State enumState;



	public Transform player;
	Animator anim;

    public float startHealth = 300f;
	private float health;
	public Image healthBar;
	public float lifeTime = 5f;

    //State enumState = State.Patrol;

   // string state = "patrol";
	public GameObject[] waypoints; // way points/amount of waypoints
	public int currentWP = 0;

	public float rotSpeed = 0.2f; //rotation speed
	public float speed = 1.5f; // move speed
	float accuracyWP = 2.0f; // accuracy when following way points

	public GameObject screenFlash; //Blink red when taking damage 
	// 3 different hurt sounds for when getting hit
	public AudioSource Hurt01; 
	public AudioSource Hurt02;
	public AudioSource Hurt03;
	public int painSound;
	public int attackTrigger;
	public bool canAttack;
	public bool isDead;

    //public Transform direction;
    Vector3 direction;


	[SerializeField]
	Transform _destination;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		isDead = false;
		health = startHealth;
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        float angle = Vector3.Angle (direction,this.transform.forward);

        currentWP = Random.Range(0, waypoints.Length);
    }

    // Update is called once per frame
    void Update () {
        
        //agent.SetDestination
        switch (enumState)
        {
            case State.Patrol:
                Patrol();
               // print("Patrol");
                break;
            case State.Idle:
                 Idle();
                //print("Idle");
                break;
            case State.Aggro:
                Aggro();
               // print("Aggro");
                break;
            default: break;
        }
        


		if (isDead == false) 
		{
			//Vector3 direction = player.position - this.transform.position;
			//direction.y = 0;
			//float angle = Vector3.Angle (direction,this.transform.forward);
        }

	}// End Update loop

    void Idle()
    {
        print("doing nothing");
    }


    void Patrol()
    {
        //Start patrolling cuz no player around
        //if (State == Patrol && waypoints.Length > 0)
        if (waypoints.Length > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
            {
                //Use this if you want them to follow each WP randomly
                currentWP = Random.Range(0, waypoints.Length);
                //Use this if you want them to follow each WP orderly
                //if (currentWP >= waypoints.Length) 
                //{
                //	currentWP = 0;
                //}
            }
            //Go to WP direction
            agent.SetDestination(waypoints[currentWP].transform.position);
        }
    }



    void Aggro()
    {
        //Start Agro
       // if (enumState = Aggro)
        //{
            //Turn towards the player
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

            if (direction.magnitude > 2) //If you are futher than 2 continue persuit.
            {
                //this.transform.Translate (0, 0,Time.deltaTime * speed);
                agent.SetDestination(player.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            { //Else Attack
                if (canAttack == true)
                {
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isWalking", false);
                    StartCoroutine(EnemyDamage());
                }
            } 
        }
        //else
        //{ // If there is no persuit than continue walking
          //  anim.SetBool("isWalking", true);
           // anim.SetBool("isAttacking", false);
            //state == Patrol;
        //}
    


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			canAttack = true;
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			canAttack = false;
		}
	}

	IEnumerator EnemyDamage(){
		print ("ThePain");
		yield return new WaitForSeconds (0.15f);
		screenFlash.SetActive (true);
		//GlobalHealth.currentHealth -= 0.5f;
		Hurt03.Play ();
		yield return new WaitForSeconds (0.05f);
		screenFlash.SetActive (false);
		yield return new WaitForSeconds (3);
	}

	private void SetDestination()
	{
		if(_destination != null)
		{
            //Vector3 targetVector = _destination.transform.position;
            //_navMeshAgent.SetDestination(targetVector);
            agent.SetDestination(waypoints[currentWP].transform.position);

        }
	}
		
	public void TakeDamege (float amount)
	{
		health -= amount;
        //state = "Agro";
        print("Agro");
        healthBar.fillAmount = health / startHealth;

		if (health <= 0f) 
		{
			Die ();
		}
	}

	public void Die()
	{
		isDead = true;
		anim.SetBool ("isDead", true);
		anim.SetBool ("isAttacking", false);
		anim.SetBool ("isWalking", false);
		Destroy (gameObject,lifeTime);
	}
}

