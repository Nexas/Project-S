using UnityEngine;
using System.Collections;

public class MainMenuTitle : MonoBehaviour {

	float origW = 640.0f;
	float origH = 480.0f;
	float halfScreenWidth = Screen.width / 2.0f;
	float halfScreenHeight = Screen.height / 2.0f;
	static public int selection;
	public int numOptions;
	
	void Start() {
		selection = 1;
		numOptions = 3;
		float scalex =  origW / (float) (Screen.width); //your scale x
		float scaley = (float) (Screen.height) / origH; //your scale y
		GUIText myText = GetComponent<GUIText>(); //find your element
		Vector2 pixOff = myText.pixelOffset; //your pixel offset on screen
		int origSizeText = myText.fontSize;
		pixOff.y = halfScreenHeight - (Screen.height * .05f);
		
		myText.pixelOffset = new Vector2(pixOff.x, pixOff.y);
		//myText.pixelOffset = new Vector2(pixOff.x*scalex, pixOff.y*scaley);
		//myText.fontSize = (int)(origSizeText * scalex);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			selection -= 1;
			if (selection == 0)
			{
				selection = numOptions;
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			selection += 1;
			if (selection > numOptions)
			{
				selection = 1;
			}
		}
	}
}
