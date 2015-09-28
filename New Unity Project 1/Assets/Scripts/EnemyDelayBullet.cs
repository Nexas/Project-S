using UnityEngine;
using System.Collections;

public class EnemyDelayBullet : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	Renderer rend;
	float fSlowTimer;	// The time this bullet will travel at a slow speed.
	float fStopTimer;	// The time this bullet will spend stopped.
	
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!rend.isVisible)
			Destroy (gameObject);

		if (fSlowTimer > 0.0f)
		{
			fSlowTimer -= Time.deltaTime;
			transform.Translate(vel * 0.33f * Time.deltaTime);
		}
		else if (fStopTimer > 0.0f)
		{
			fStopTimer -= Time.deltaTime;
		}
		else
			transform.Translate(vel * Time.deltaTime);	
	}
	
	public void SetVelocity(Vector3 _vel)
	{
		vel.x = _vel.x;
		vel.y = _vel.y;
		vel.z = _vel.z;
	}

	public void SetSlowTimer(float _timer)
	{
		fSlowTimer = _timer;
	}

	public void SetStopTimer(float _timer)
	{
		fStopTimer = _timer;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (this.tag == "playerBullet" && collision.collider.tag == "player")
		{
			// Do nothing
		}
		
		if (this.tag == "playerBullet" && collision.collider.tag == "enemy") 
		{
		}
	}
}
