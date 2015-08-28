using UnityEngine;
using System.Collections;

public class PlayerBomb : MonoBehaviour {

	float fDeathTimer;

	// Use this for initialization
	void Start () {
		fDeathTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		fDeathTimer += Time.deltaTime;

		if (fDeathTimer >= 1.25f)
		{
			Destroy(gameObject);
		}

		transform.localScale = new Vector3(transform.localScale.x + 0.45f, transform.localScale.y, transform.localScale.z + 0.45f);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "enemy" || collider.tag == "enemyBullet" || collider.tag == "Environment")
		{
			Destroy(collider.gameObject);
		}
	}
}
