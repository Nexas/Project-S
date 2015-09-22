using UnityEngine;
using System.Collections;

public class ShieldedEnemy : MonoBehaviour {

	int nHealth;
	float bulletTimer;
	public Transform eBullet;
	bool bOnScreen;
	bool bRecharging;		// True if all waves of shots have been fired.
	int nShotAmount;		// Number of waves of shots to be fired.
	float fShotCounter;		// Time before next wave of shots can be fired.
	Vector3 camRight;
	Vector3 camLeft;
	Vector3 camBottom;
	Vector3 camTop;
	public Transform powerup;
	public Transform deathEffect;
	public Transform shield;
	
	// Use this for initialization
	void Start () {
		nHealth = 10;
		bulletTimer = 1.35f;
		nShotAmount = 3;

		// Create the 4 shields around the enemy,moving the position for each of the cardinal directions.
		Vector3 pos = transform.position;
		Transform t;
		GameObject _shieldObj;
		pos.x += 12.0f;

		t = Instantiate(shield, pos, transform.rotation) as Transform;
		_shieldObj = t.gameObject;
		_shieldObj.GetComponent<EnemyShield> ().SetRotatePosition(transform.position);

		pos.x -= 24.0f;

		t = Instantiate(shield, pos, transform.rotation) as Transform;
		_shieldObj = t.gameObject;
		_shieldObj.GetComponent<EnemyShield> ().SetRotatePosition(transform.position);

		pos.x += 12.0f;
		pos.z += 12.0f;

		t = Instantiate(shield, pos, transform.rotation) as Transform;
		_shieldObj = t.gameObject;
		_shieldObj.GetComponent<EnemyShield> ().SetRotatePosition(transform.position);

		pos.z -= 24.0f;

		t = Instantiate(shield, pos, transform.rotation) as Transform;
		_shieldObj = t.gameObject;
		_shieldObj.GetComponent<EnemyShield> ().SetRotatePosition(transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 100.0f));
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		
		bulletTimer -= Time.deltaTime;
		
		if (bulletTimer <= 0.0f)
		{
			if (nShotAmount > 0)
			{
				fShotCounter += Time.deltaTime;

				if (fShotCounter >= 0.15f)
				{
					fShotCounter = 0.0f;
					nShotAmount -= 1;
					Vector3 toPlayer = GameGod.playerPos;
					toPlayer -= transform.position;
					toPlayer.Normalize();
					toPlayer *= 50.0f;
					SpawnBullet(toPlayer);
					toPlayer = Quaternion.AngleAxis(-15, Vector3.up) * toPlayer;
					SpawnBullet(toPlayer);
					toPlayer = Quaternion.AngleAxis(30, Vector3.up) * toPlayer;
					SpawnBullet(toPlayer);
				}
			}
			else
			{
				nShotAmount = 3;
				bulletTimer = 1.35f;
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
