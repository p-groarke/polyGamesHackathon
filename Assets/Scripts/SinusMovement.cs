using UnityEngine;
using System.Collections;

public class SinusMovement : MonoBehaviour {

	float amplitude = 2f;
	float frequency = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += amplitude*(Mathf.Sin(2*Mathf.PI*frequency*Time.time) - Mathf.Sin(2*Mathf.PI*frequency*(Time.time - Time.deltaTime)))*transform.position;
	}
}
