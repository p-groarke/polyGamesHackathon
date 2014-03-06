using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed = -0.02f;
	public int HP = 3;
	private float lastTime = 0f;
	
	// Use this for initialization
	void Start () {
		
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
<<<<<<< HEAD
		//print("Collision");
=======
>>>>>>> master
		if (collision.gameObject.CompareTag ("Player") == true) 
			speed = 0;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
<<<<<<< HEAD
		//print("Collision");
=======
>>>>>>> master
		if (collision.gameObject.CompareTag ("Player") == true) 
			speed = -0.02f;
	}


	// Update is called once per frame
	void Update () {
		print (speed);
		Vector3 newPosition = transform.position;
		if (lastTime != Time.time) 
		{
			newPosition.x = transform.position.x + speed;
			transform.position = newPosition;
		}

		float vertExtent = GameObject.Find("Camera").GetComponent<Camera>().camera.orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		if (transform.position.x < (horzExtent * -1) - 3) 
		{
			Destroy(gameObject);
		}
		lastTime = Time.time;
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
