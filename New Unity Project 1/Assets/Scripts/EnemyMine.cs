using UnityEngine;
using System.Collections;

public class EnemyMine : MonoBehaviour {

	public Transform eBullet;
	public Transform effect;

    Vector3 camBottom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));

        if (transform.position.z < camBottom.z - 2.5f)
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

	void DetonateMine()
	{
		SpawnBullet (new Vector3 (0.0f, 0.0f, 50.0f));
		SpawnBullet (new Vector3 (50.0f, 0.0f, 50.0f));
		SpawnBullet (new Vector3 (50.0f, 0.0f, 0.0f));
		SpawnBullet (new Vector3 (50.0f, 0.0f, -50.0f));
		SpawnBullet (new Vector3 (0.0f, 0.0f, -50.0f));
		SpawnBullet (new Vector3 (-50.0f, 0.0f, -50.0f));
		SpawnBullet (new Vector3 (-50.0f, 0.0f, 0.0f));
		SpawnBullet (new Vector3 (-50.0f, 0.0f, 50.0f));
		Instantiate (effect, transform.position, transform.rotation);
	}

	void OnTriggerEnter(Collider collision)
	{
		if (tag == "enemy" && collision.GetComponent<Collider>().tag == "player")
		{
			DetonateMine();
			Destroy(this.gameObject);
		}
		
		if (tag == "enemy" && collision.GetComponent<Collider>().tag == "playerBullet")
		{
			Destroy(collision.gameObject);
			DetonateMine();
			Destroy(this.gameObject);
		}
	}
}
