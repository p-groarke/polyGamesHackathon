using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

using System.Collections;



public class AudioHandler : MonoBehaviour {

	// Static reference to singleton object
	public static AudioHandler instance;
	public AudioClip[] myClip;
	
	// Use this for initialization
	
	void Start ()
	{
		instance = gameObject.GetComponent<AudioHandler>();
	}

	public void playSound ()
	{
		audio.PlayOneShot(myClip[Random.Range(0,myClip.Length)]);
	}
	
	
}
