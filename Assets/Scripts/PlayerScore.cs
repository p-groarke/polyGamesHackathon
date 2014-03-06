using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

	public int score = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 24;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.textColor = Color.red; 
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + (int)score, myStyle);
	}

	public void addPoints(int points)
	{
		score += points;
	}
}
