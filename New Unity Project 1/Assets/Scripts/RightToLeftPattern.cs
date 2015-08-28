using UnityEngine;
using System.Collections;

public class RightToLeftPattern : MonoBehaviour {

	Vector3 camLeft;
	Vector3 camTop;
	Vector3 camRight;
	Vector3 camBottom;
	
	bool isSpawning;

	float fInitalZPos;

	void Awake()
	{
		fInitalZPos = transform.position.z;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		camLeft = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, 0.0f, 100.0f));
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));
		camRight = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, 0.0f, 100.0f));
		camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 100.0f));
		
		if (camTop.z + 2.0f >= fInitalZPos)
		{
			isSpawning = true;
		}
		
		if (isSpawning)
			transform.Translate( -5.0f * Time.deltaTime, 0.0f, 0.0f);
	}
}
