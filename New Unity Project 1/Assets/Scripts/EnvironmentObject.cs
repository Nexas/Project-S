using UnityEngine;
using System.Collections;

public class EnvironmentObject : MonoBehaviour {

	public Material[] materials;
	int health;					// Health of the Object.
	float flashTimer;			// How long to flash white.
	bool isFlashing;			// Whether or not to flash white.

	// Use this for initialization
	void Start () {
		health = 50;
		flashTimer = 0.05f;
		isFlashing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFlashing)
		{
			flashTimer -= Time.deltaTime;
			if (flashTimer <= 0.0f)
			{
				flashTimer = 0.05f;
				isFlashing = false;
				GetComponent<Renderer>().material = materials[0];
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "playerBullet" && tag == "Environment")
		{
			Destroy(collider.gameObject);
			health -= 1;
			if (health <= 0)
			{
				Destroy(gameObject);
			}

			if (!isFlashing)
			{
				isFlashing = true;
				GetComponent<Renderer>().material = materials[1];
			}
		}
	}
}
