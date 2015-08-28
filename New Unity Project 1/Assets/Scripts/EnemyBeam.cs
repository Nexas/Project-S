using UnityEngine;
using System.Collections;

public class EnemyBeam : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	public float haltTimer;	// Time before the bullet begins moving along its velocity.
	float deathTimer;	// Time before the bullet is destroyed.
	bool bHalted;
	float speed;		// The speed at which the beam is traveling.

	void Awake()
	{
		haltTimer = 1.0f;
		bHalted = true;
		deathTimer = 10.0f;
		speed = 250.0f;
	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		haltTimer -= Time.deltaTime;
		if (deathTimer <= 0.0f)
		{
			deathTimer = 0.0f;
			Destroy (gameObject);
		}
		if (haltTimer <= 0.0f && bHalted)
		{
			bHalted = false;
		}

		if (!bHalted)
		{
			Vector3 forward = Vector3.forward * speed;
			transform.Translate(forward * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.forward * 15.0f * Time.deltaTime);
		}

	}

	public void SetVelocity(Vector3 _vel)
	{
		vel.x = _vel.x;
		vel.y = _vel.y;
		vel.z = _vel.z;
	}

	public void SetSpeed(float _speed)
	{
		speed = _speed;
	}
}
