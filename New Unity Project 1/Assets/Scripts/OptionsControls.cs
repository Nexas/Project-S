using UnityEngine;
using System.Collections;

public class OptionsControls : MonoBehaviour {

	float origW = 640.0f;
	float origH = 480.0f;
	float halfScreenWidth = Screen.width / 2.0f;
	float halfScreenHeight = Screen.height / 2.0f;
	const int optionNumber = 1;
	
	void Start() {
		float scalex =  origW / (float) (Screen.width); //your scale x
		float scaley = (float) (Screen.height) / origH; //your scale y
		GUIText myText = GetComponent<GUIText>(); //find your element
		Vector2 pixOff = myText.pixelOffset; //your pixel offset on screen
		int origSizeText = myText.fontSize;
		pixOff.x = -halfScreenWidth + (Screen.width * .05f);
		
		myText.pixelOffset = new Vector2(pixOff.x, pixOff.y);
		//myText.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		//myText.fontSize = (int)(origSizeText * scalex);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (OptionsTitle.selection == optionNumber)
		{
			this.GetComponent<GUIText>().color = Color.red;
		}
		else
		{
			this.GetComponent<GUIText>().color = Color.white;
		}
	}
}
