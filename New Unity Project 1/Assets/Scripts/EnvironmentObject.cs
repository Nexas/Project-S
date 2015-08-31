using UnityEngine;
using System.Collections;

public class EnvironmentObject : MonoBehaviour {

	public Material[] materials;
	int health;					// Health of the Object.
	float flashTimer;			// How long to flash white.
	bool isFlashing;			// Whether or not to flash white.

    Vector3 camBottom;

	// Use this for initialization
	void Start () {
		health = 50;
		flashTimer = 0.05f;
		isFlashing = false;
	}
	
	// Update is called once per frame
	void Update () {
        camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));

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

        if (transform.position.z < camBottom.z - 3.33f)
        {
            Destroy(this.gameObject);
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
