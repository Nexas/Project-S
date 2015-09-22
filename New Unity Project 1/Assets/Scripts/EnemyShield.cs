using UnityEngine;
using System.Collections;

public class EnemyShield : MonoBehaviour {

	int nHealth;
	Vector3 rotatePosition;

	// Use this for initialization
	void Start () {
		nHealth = 15;
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (rotatePosition, Vector3.up, 90 * Time.deltaTime);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "playerBullet")
		{
			nHealth -= 1;
			Destroy(collider.gameObject);

			if (nHealth <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}

	public void SetRotatePosition(Vector3 vec)
	{
		rotatePosition = vec;
	}
}
