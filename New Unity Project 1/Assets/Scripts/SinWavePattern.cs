using UnityEngine;
using System.Collections;

public class SinWavePattern : MonoBehaviour {

	Vector3 camLeft;
	Vector3 camTop;
	Vector3 camRight;
	Vector3 camBottom;
	float fTimer;

	bool isSpawning;

	// Use this for initialization
	void Start () {
		fTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		fTimer += Time.deltaTime * 3.25f;
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		
		if (camTop.z + 2.0f >= transform.position.z)
		{
			isSpawning = true;
		}

		if (isSpawning)
			transform.Translate( Mathf.Sin(fTimer) * 0.55f, 0.0f, 0.0f);
	}
}
