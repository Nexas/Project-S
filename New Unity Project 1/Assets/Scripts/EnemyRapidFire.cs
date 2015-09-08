using UnityEngine;
using System.Collections;

public class EnemyRapidFire : MonoBehaviour {

	int nHealth;
	float bulletTimer;
	float bulletCooldown;
	public Transform eBullet;
	bool bOnScreen;
    Vector3 camRight;
    Vector3 camLeft;
    Vector3 camBottom;
    Vector3 camTop;
	int numShots;
	bool readyFire;
	public Transform powerup;
	public Transform deathEffect;
	
	// Use this for initialization
	void Start () {
		nHealth = 10;
		bulletTimer = .10f;
		bulletCooldown = 2.25f;
		numShots = 5;
		readyFire = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
        camLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
        camTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 100.0f));
        camRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 100.0f));
        camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		
		bulletTimer -= Time.deltaTime;

		if (readyFire)
		{
			if (bulletTimer <= 0.0f && numShots > 0)
			{
				Vector3 toPlayer = GameGod.playerPos;
				toPlayer -= transform.position;
				toPlayer.Normalize();
				toPlayer *= 50.0f;
				bulletTimer = .10f;
				numShots -= 1;
				SpawnBullet(toPlayer);
			}

			if (numShots <= 0)
			{
				readyFire = false;
				numShots = 5;
			}
		}
		else
		{
			bulletCooldown -= Time.deltaTime;

			if (bulletCooldown <= 0.0f)
			{
				bulletCooldown = 2.25f;
				readyFire = true;
			}
		}
		
		if (GetHealth() <= 0)
		{
			int powerupNumber = Random.Range(4, 6);
			for (int i = 0; i < powerupNumber; ++i)
			{
				Vector3 direction = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
				Transform t = Instantiate(powerup, transform.position, transform.rotation) as Transform;
				GameObject pow = t.gameObject;
				pow.GetComponent<WeaponPowerup>().SetVelocity(direction);
			}
			Instantiate(deathEffect, transform.position, transform.rotation);
			Destroy(gameObject);
		}

        if (transform.position.x < camLeft.x - 2.5f || transform.position.x > camRight.x + 2.5f || transform.position.z < camBottom.z - 2.5f)
        {
            Destroy(this.gameObject);
        }
	}
	
	void SpawnBullet(Vector3 vel)
	{
		Transform t = Instantiate(eBullet, this.transform.position, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBullet>().SetVelocity(vel);
	}
	
	public int GetHealth()
	{
		return nHealth;
	}
	
	public void SetHealth(int health)
	{
		nHealth = health;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (tag == "enemy" && collision.collider.tag == "enemyBullet")
		{
			// Do nothing
		}
		
		if (tag == "enemy" && collision.collider.tag == "playerBullet")
		{
			Destroy(collision.gameObject);
			SetHealth(GetHealth() - 1);
		}
	}
	
	void OnTriggerEnter(Collider collision)
	{
		if (tag == "enemy" && collision.GetComponent<Collider>().tag == "enemyBullet")
		{
			// Do nothing
		}
		
		if (tag == "enemy" && collision.GetComponent<Collider>().tag == "playerBullet")
		{
			Destroy(collision.gameObject);
			SetHealth(GetHealth() - 1);
		}
	}
}
