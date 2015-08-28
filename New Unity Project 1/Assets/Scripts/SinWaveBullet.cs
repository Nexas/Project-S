using UnityEngine;
using System.Collections;

public class SinWaveBullet : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	float deathTimer;
	float fTimer;
	float delayTimer; //Time to delay before moving as a sin wave.
	
	// Use this for initialization
	void Start () {
		deathTimer = 2.0f;
		fTimer = 0.0f;
		delayTimer = 0.5f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		fTimer += Time.deltaTime * 4.25f;
		if (delayTimer >= 0.0f)
			delayTimer -= Time.deltaTime;
		// TODO: Remove death timers when actual enemies are created.
		deathTimer -= Time.deltaTime;
		if (deathTimer <= 0.0f)
		{
			deathTimer = 0.0f;
			Destroy (gameObject);
		}
		Vector3 forwardVec = Vector3.forward;
		if (delayTimer <= 0.0f)
			forwardVec.x = Mathf.Sin(fTimer) * 0.55f;
		forwardVec *= 50.0f;
		transform.Translate(forwardVec * Time.deltaTime);	
	}
	
	public void SetVelocity(Vector3 _vel)
	{
		vel.x = _vel.x;
		vel.y = _vel.y;
		vel.z = _vel.z;
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
