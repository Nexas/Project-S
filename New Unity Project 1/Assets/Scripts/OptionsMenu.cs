using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	public static bool isKeyboard = true;
	public static float fBGMVol = 50.0f;
	public static float fSFXVol = 50.0f;

	void Awake()
	{
		DontDestroyOnLoad(transform.GetComponent<GUIText>());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
