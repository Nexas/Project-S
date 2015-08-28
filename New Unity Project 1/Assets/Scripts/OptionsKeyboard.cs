using UnityEngine;
using System.Collections;

public class OptionsKeyboard : MonoBehaviour {

	float origW = 640.0f;
	float origH = 480.0f;
	float halfScreenWidth = Screen.width / 2.0f;
	float halfScreenHeight = Screen.height / 2.0f;
	
	void Start() {
		float scalex =  origW / (float) (Screen.width); //your scale x
		float scaley = (float) (Screen.height) / origH; //your scale y
		GUIText myText = GetComponent<GUIText>(); //find your element
		Vector2 pixOff = myText.pixelOffset; //your pixel offset on screen
		int origSizeText = myText.fontSize;
		pixOff.x = halfScreenWidth - (Screen.width * .50f);
		
		myText.pixelOffset = new Vector2(pixOff.x, pixOff.y);
		//myText.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		//myText.fontSize = (int)(origSizeText * scalex);
	}

	void OnMouseEnter()
	{
		if (!OptionsMenu.isKeyboard)
		{
			this.GetComponent<GUIText>().color = Color.yellow;
		}
	}
	
	void OnMouseExit()
	{
		if (!OptionsMenu.isKeyboard)
		{
			this.GetComponent<GUIText>().color = Color.white;
		}
		else
		{
			this.GetComponent<GUIText>().color = Color.red;
		}
	}
	
	void OnMouseUp()
	{
		OptionsMenu.isKeyboard = true;
	}

	void Update()
	{
		if (OptionsMenu.isKeyboard)
			this.GetComponent<GUIText>().color = Color.red;
		else
			this.GetComponent<GUIText>().color = Color.white;
	}
}
