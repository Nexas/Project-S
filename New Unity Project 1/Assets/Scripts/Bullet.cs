using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!rend.isVisible)
			Destroy (gameObject);

		transform.Translate(vel * Time.deltaTime);	
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
