using UnityEngine;
using System.Collections;

public class EnemyDeathEffect : MonoBehaviour {

	float fDeathTimer;
	
	// Use this for initialization
	void Start () {
		fDeathTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		fDeathTimer += Time.deltaTime;
		
		if (fDeathTimer >= 0.25f)
		{
			Destroy(gameObject);
		}
		
		transform.localScale = new Vector3(transform.localScale.x + 0.045f, transform.localScale.y, transform.localScale.z + 0.045f);
	}
}
