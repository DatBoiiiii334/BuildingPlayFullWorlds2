using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour 
{
	public float startHealth = 100f;
	private float health;

	[Header("Unity stuff")]
	public Image healthBar;

	void Start()
	{
		health = startHealth;	
	}

	public void TakeDamege (float amount)
	{
		health -= amount;

		healthBar.fillAmount = health / startHealth;

		if (health <= 0f) 
		{
			Die ();
		}
	}

	public void Die()
	{
		Destroy (gameObject);
	}
}
