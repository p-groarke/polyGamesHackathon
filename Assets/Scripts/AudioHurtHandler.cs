using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHurtHandler : MonoBehaviour {

	// Static reference to singleton object
	public static AudioHurtHandler instance;
	public AudioClip[] myClip;
	
	// Use this for initialization
	
	void Start ()
	{
		instance = gameObject.GetComponent<AudioHurtHandler>();
	}
	
	public void playSound ()
	{
		audio.PlayOneShot(myClip[Random.Range(0,myClip.Length)]);
		//wait ();
	}
	
	IEnumerator wait()
	{
		yield return new WaitForSeconds(1);
	}
}
