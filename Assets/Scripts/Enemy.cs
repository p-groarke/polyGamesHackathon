using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	bool isDead= false;
	public float speed = -0.02f;
	public int HP = 3;
	private float lastTime = 0f;
	public GameObject ennemyHead;
	private PlayerScore score;
	public GameObject[] enemyDeath;
	
	// Use this for initialization
	void Start () {
		score = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScore> ();
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = 0;
			Damage (1);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = -0.02f;
			
			if (HP == 0) 
			{
				Death();
				isDead = true;
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
		score.addPoints(5);
	}

	void Death()
	{
		score.addPoints(50);
		GameObject newEnnemyHead = (GameObject)Instantiate (ennemyHead, transform.position, transform.rotation);
		Vector3 headVelocity = newEnnemyHead.rigidbody2D.velocity;
		headVelocity.x += Random.Range (-10, 10);
		headVelocity.y += Random.Range (0, 40);
		newEnnemyHead.rigidbody2D.velocity = headVelocity;

		//Vector3 newPosition = transform.position;
		//newPosition.x = transform.position.x + 5f;

		int deathIndex = Random.Range(0, enemyDeath.Length);
		Instantiate(enemyDeath[deathIndex], transform.position, transform.rotation);

		Destroy(gameObject);
	}

}
