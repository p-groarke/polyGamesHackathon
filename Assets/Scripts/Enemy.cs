using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	bool isDead= false;
	int lastHP;
	public float speed = -0.02f;
	public int HP = 3;
	public GameObject[] enemyDeath;
	public GameObject ennemyHead;

	private float lastTime = 0f;
	private PlayerScore score;
	private GameObject newEnnemyHead;
	private float vertExtent;
	private float horzExtent;
	
	// Use this for initialization
	void Start () {
		score = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScore> ();
		lastHP = HP;
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		//print("Collision");r
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = 0;
			Damage (1);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		//print("Collision");
		if (collision.gameObject.CompareTag ("Player") == true) 
		{
			speed = -0.02f;
			
			if (HP <= 0) 
			{
				Death();
				isDead = true;
			}
		}
	}


	// Update is called once per frame
	void Update () {
		//Play hurt sound
		if (lastHP > HP && HP != 0)
		{
			//AudioHurtHandler.instance.playSound();
			lastHP = HP;
		}
		Vector3 newPosition = transform.position;
		if (lastTime != Time.time) 
		{
			newPosition.x = transform.position.x + speed;
			transform.position = newPosition;
		}

		vertExtent = GameObject.Find("Main Camera").GetComponent<Camera>().camera.orthographicSize;  
		horzExtent = vertExtent * Screen.width / Screen.height;
		if (transform.position.x < (horzExtent * -1) - 3) 
		{
			Destroy(gameObject);
		}
		if (newEnnemyHead != null && ((newEnnemyHead.transform.position.x > horzExtent) || (newEnnemyHead.transform.position.x < -horzExtent))) 
		{
			Destroy(newEnnemyHead);
		}
		lastTime = Time.time;
	}

	public void Damage(int hp)
	{
		print ("damage"+ hp);
		// Reduce the number of hit points by one.
		HP -= hp;
		if (HP <= 0)
			Death();
		score.addPoints(5);
	}

	void Death()
	{
		score.addPoints(50);
		GameObject newEnnemyHead = (GameObject)Instantiate (ennemyHead, transform.position, transform.rotation);
		AudioDeathHandler.instance.playSound();
		newEnnemyHead = (GameObject)Instantiate (ennemyHead, transform.position, transform.rotation);
		Vector3 headVelocity = newEnnemyHead.rigidbody2D.velocity;
		headVelocity.x += Random.Range (-10, 10);
		headVelocity.y += Random.Range (0, 30);
		newEnnemyHead.rigidbody2D.velocity = headVelocity;

		//Vector3 newPosition = transform.position;
		//newPosition.x = transform.position.x + 5f;

		int deathIndex = Random.Range(0, enemyDeath.Length);
		Instantiate(enemyDeath[deathIndex], transform.position, transform.rotation);

		Destroy(gameObject);
	}

}
