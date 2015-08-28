using UnityEngine;
using System.Collections;

public class GameLives : MonoBehaviour {

	float halfScreenWidth = Screen.width / 2.0f;
	float halfScreenHeight = Screen.height / 2.0f;
	string text;
	static bool created;

	void Awake()
	{
		if (created)
			return;
		DontDestroyOnLoad(this);
	}
	
	void Start() {
		created = true;
		GUIText myText = GetComponent<GUIText>(); //find your element
		text = myText.text;
		Vector2 pixOff = myText.pixelOffset; //your pixel offset on screen
		int origSizeText = myText.fontSize;
		pixOff.x = -halfScreenWidth + (Screen.width * .1f);
		pixOff.y = halfScreenHeight - (Screen.height * .05f);
		
		myText.pixelOffset = new Vector2(pixOff.x, pixOff.y);
		myText.text = text + GameGod.lives.ToString();
		//myText.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		//myText.fontSize = (int)(origSizeText * scalex);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<GUIText>().text = text + GameGod.lives.ToString();
	}
}
