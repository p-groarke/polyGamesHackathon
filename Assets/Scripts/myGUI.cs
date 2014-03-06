using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour {


	void Start () {
	}

	void Update () {
	
	}
	// Use this for initialization
	void OnGUI () {
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);  //Keeps the button looking like a button
		buttonStyle.fontSize = 30;  //changes font size of button
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.fontStyle = FontStyle.Bold;
		if (GUI.Button (new Rect (Screen.width/2 - 75,Screen.height / 2 - 150,150,100), "Play", buttonStyle)) {
			Application.LoadLevel ("Level1"); 
		}
		if(GUI.Button (new Rect (Screen.width/2 - 75,Screen.height / 2 + 50,150,100), "Quit", buttonStyle)) {
			Application.Quit();
		}
	}

}
