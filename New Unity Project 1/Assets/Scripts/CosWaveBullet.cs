using UnityEngine;
using System.Collections;

public class CosWaveBullet : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
    Renderer rend;
	float delayTimer; //Time to delay before moving as a cosine wave.
	
	// Use this for initialization
	void Start () {
		delayTimer = 0.0f;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
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
            transform.Translate(new Vector3(Mathf.Cos(GameGod.fGameTimer * 4.25f) * 0.55f, 0.0f, 0.0f));
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
