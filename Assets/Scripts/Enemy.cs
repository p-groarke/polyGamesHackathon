using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	bool isDead= false;
	public float speed = -0.02f;
	public int HP = 3;
	public GameObject animation;
	private float lastTime = 0f;
	public GameObject ennemyHead;
	
	// Use this for initialization
	void Start () {
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = 0;
			Damage (1);
			if (HP == 0) 
			{
				Death();
				isDead = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = -0.02f;
			
			if (isDead) {
				speed = 0.01f;
			}
		}
	}


	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		if (lastTime != Time.time) 
		{
			newPosition.x = transform.position.x + speed;
			transform.position = newPosition;
		}

		float vertExtent = GameObject.Find("Main Camera").GetComponent<Camera>().camera.orthographicSize;  
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
		GameObject newEnnemyHead = (GameObject)Instantiate (ennemyHead, transform.position, transform.rotation);
		Vector3 headVelocity = newEnnemyHead.rigidbody2D.velocity;
		headVelocity.x += Random.Range (-10, 10);
		headVelocity.y += Random.Range (0, 40);
		newEnnemyHead.rigidbody2D.velocity = headVelocity;
	}


}
