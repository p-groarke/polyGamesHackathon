using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour {

	void Start () {

	}

	void Update () {
	
	}
	// Use this for initialization
	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width/2 - 75,Screen.height / 2 - 150,150,100), "Play")) {
			Application.LoadLevel ("Level1"); 
		}
		if(GUI.Button (new Rect (Screen.width/2 - 75,Screen.height / 2 + 50,150,100), "Quit")) {
			Application.Quit();
		}
	}

}
