using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float speed = 0.02f;
	public float maxHeight = 0.1f;
	private float lastTime = 0f;
	private float initialY;
	private bool up = true;

	// Use this for initialization
	void Start () {
		initialY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = transform.position;
		if (lastTime != Time.time) 
		{
			if (transform.position.y + speed < maxHeight && up == true)
			{
				newPosition.y = transform.position.y + speed;
				transform.position = newPosition;
			}
			else
			{
				up = false;
				if (transform.position.y - speed > initialY && up == false)
				{
					newPosition.y = transform.position.y - speed;
					transform.position = newPosition;
				}
				else
					up = true;
			}
		}

		lastTime = Time.time;
	}
}
