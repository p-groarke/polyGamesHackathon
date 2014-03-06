using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform player;		// Reference to the player.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
	{
		// Set the position to the player's position with the offset.
		transform.position = player.position + offset;
		int barScale = GameObject.Find ("hero").GetComponent<HeroControl> ().HP;
		GameObject.FindGameObjectWithTag ("HealthBar").transform.localScale = new Vector3 (barScale*0.01f, 1f, 1f);
	}
}
