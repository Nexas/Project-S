using UnityEngine;
using System.Collections;

public class EnemySinWave : MonoBehaviour {

	int nHealth;
	float bulletTimer;
	float bulletCooldown;
	public Transform eBullet;
	public Transform eCosBullet;
	bool bOnScreen;
	Vector3 camRight;
	Vector3 camLeft;
	Vector3 camBot;
	int numShots;
	bool readyFire;
	Vector3 dirShot;	// The direction the wave shot will be fired at.
	public Transform powerup;
	
	// Use this for initialization
	void Start () {
		nHealth = 10;
		bulletTimer = .10f;
		bulletCooldown = 2.25f;
		numShots = 5;
		readyFire = true;
		dirShot = GameGod.playerPos;
	}
	
	// Update is called once per frame
	void Update () 
	{
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camBot = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100.0f));
		
		bulletTimer -= Time.deltaTime;

		// TODO: Remove this line of code!
		transform.Translate(new Vector3(0.0f, 0.0f, 10.0f) * Time.deltaTime);
		
		if (readyFire)
		{
			if (bulletTimer <= 0.0f && numShots > 0)
			{
				dirShot -= transform.position;
				dirShot.Normalize();
				dirShot *= 50.0f;
				bulletTimer = .10f;
				numShots -= 1;
				SpawnBullet(dirShot);
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
				dirShot = GameGod.playerPos;
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
			Destroy(gameObject);
		}
		
		if (transform.position.z < camLeft.z - 2.5f)
		{
			Destroy(this.gameObject);
		}
	}
	
	void SpawnBullet(Vector3 vel)
	{
		Vector3 pos = transform.GetComponent<Renderer>().bounds.center;
		Transform t = Instantiate(eBullet, pos, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<SinWaveBullet>().SetVelocity(vel);
		bul.transform.LookAt(GameGod.playerPos);

		t = Instantiate(eCosBullet, pos, transform.rotation) as Transform;
		bul = t.gameObject;
		bul.GetComponent<CosWaveBullet>().SetVelocity(vel);
		bul.transform.LookAt(GameGod.playerPos);
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
