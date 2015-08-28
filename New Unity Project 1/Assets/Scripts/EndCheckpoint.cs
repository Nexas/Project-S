using UnityEngine;
using System.Collections;

public class EndCheckpoint : MonoBehaviour {

	Vector3 camTop;
	public bool bBossLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		camTop = Camera.main.ScreenToWorldPoint(new Vector3 (0.0f, Screen.height, 100.0f));

		if (camTop.z + 2.0f >= transform.position.z)
		{
			if (bBossLevel)
			{
				GameGod.bLevelScrolling = false;
			}
			else
			{
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}
	}
}
