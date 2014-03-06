using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float deplacement = -0.02f;
	public int HP = 3;
	
	// Use this for initialization
	void Start () {
		
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		//print("Collision");
		if (collision.gameObject.CompareTag ("Player") == true) 
			deplacement = 0;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		//print("Collision");
		if (collision.gameObject.CompareTag ("Player") == true) 
			deplacement = -0.02f;
	}


	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		newPosition.x = transform.position.x + deplacement;
		transform.position = newPosition;

		float vertExtent = GameObject.Find("Camera").GetComponent<Camera>().camera.orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		if (transform.position.x < (horzExtent * -1) - 3) 
		{
			Destroy(gameObject);
		}
	}

	public void Damage(int hp)
	{
		// Reduce the number of hit points by one.
		HP -= hp;
	}

	void Death()
	{
		
	}


}
