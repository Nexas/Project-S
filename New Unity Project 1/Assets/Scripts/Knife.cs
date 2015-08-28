using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour {

	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!rend.isVisible)
			Destroy (gameObject);

		transform.Translate(new Vector3(0.0f, 0.0f, 100.0f) * Time.deltaTime);
		
	}
}
