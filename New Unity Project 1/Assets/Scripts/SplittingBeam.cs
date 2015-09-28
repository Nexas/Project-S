using UnityEngine;
using System.Collections;

public class SplittingBeam : MonoBehaviour {

	Vector3 vel;	// Velocity of the bullet.
	float deathTimer;	// Time before the bullet is destroyed.
	public float spawnTimer;	// Time between each 4 beam spawn.
	public Transform eBeam;
	Vector3 camBottom;
	
	// Use this for initialization
	void Start () {
		deathTimer = 10.0f;
		spawnTimer = 1.25f;
	}
	
	// Update is called once per frame
	void Update () {
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));

		deathTimer -= Time.deltaTime;
		spawnTimer -= Time.deltaTime;
		if (deathTimer <= 0.0f)
		{
			deathTimer = 0.0f;
			Destroy (gameObject);
		}

		if (transform.position.z < camBottom.z)
		{
			Destroy(gameObject);
		}

		if (spawnTimer <= 0.0f)
		{
			SpawnBeam(125.0f, -45);
			SpawnBeam(125.0f, 45);
			SpawnBeam(125.0f, 135);
			SpawnBeam(125.0f, -135);
			spawnTimer = 1.25f;
		}

		transform.Translate(new Vector3(0.0f, 0.0f, -50.0f) * Time.deltaTime);

	}

	void SpawnBeam(float speed, float rotation)
	{
		Transform t = Instantiate(eBeam, transform.position, transform.rotation) as Transform;
		GameObject bul = t.gameObject;
		bul.GetComponent<EnemyBeam>().SetSpeed (speed);
		bul.GetComponent<EnemyBeam>().haltTimer = 0.75f;
		bul.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

	}
	
	public void SetVelocity(Vector3 _vel)
	{
		vel.x = _vel.x;
		vel.y = _vel.y;
		vel.z = _vel.z;
	}
}
