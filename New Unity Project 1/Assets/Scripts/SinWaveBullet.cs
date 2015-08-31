using UnityEngine;
using System.Collections;

public class SinWaveBullet : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	Renderer rend;
	float fTimer;
	float delayTimer; //Time to delay before moving as a sin wave.
	
	// Use this for initialization
	void Start () {
		fTimer = 0.0f;
		delayTimer = 0.0f;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		fTimer += Time.deltaTime * 4.25f;
		if (delayTimer >= 0.0f)
			delayTimer -= Time.deltaTime;
        if (!rend.isVisible)
        {
            Destroy(gameObject);
        }
		Vector3 forwardVec = Vector3.forward;
        if (delayTimer <= 0.0f)
        {
            //forwardVec.x = Mathf.Sin(fTimer) * 0.55f;
            transform.Translate(new Vector3(Mathf.Sin(fTimer) * 0.55f, 0.0f, 0.0f));
        }
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
