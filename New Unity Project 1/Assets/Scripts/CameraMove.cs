using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	Vector3 moveVec;

	// Use this for initialization
	void Start () {
		moveVec = new Vector3(0.0f, 10.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameGod.bLevelScrolling)
			transform.Translate(moveVec * Time.deltaTime);

		if (Input.GetKey(KeyCode.L))
		{
			transform.position = new Vector3(0.0f, 100.0f, 1700.0f);
		}
	}
}
