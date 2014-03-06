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
		buttonStyle.fontSize = 40;  //changes font size of button
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.fontStyle = FontStyle.Bold;
		if (GUI.Button (new Rect (Screen.width/2 - 150,Screen.height / 2 - 150,300,100), "Play", buttonStyle)) {
			Application.LoadLevel ("Level1"); 
		}
		if(GUI.Button (new Rect (Screen.width/2 - 150,Screen.height / 2 + 50,300,100), "Quit", buttonStyle)) {
			Application.Quit();
		}
	}

}
