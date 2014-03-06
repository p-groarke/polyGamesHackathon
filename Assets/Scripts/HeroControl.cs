using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	public float speed = 0.1F;
	public float flyDuration = 5.0F;

	Animator animator;
	Vector2 initPosition;
	Vector3 gotoPosition;
	bool goingToPos;
	float startFlyTime;

	// Use this for initialization
	void Start () 
	{
		initPosition = transform.position;
		animator = this.GetComponent<Animator>();
		goingToPos = false;

	}

	void defaultState()
	{
		animator.SetInteger("State", 0);
		goingToPos = false;
		transform.position = initPosition;
	}

	void nextState()
	{
		animator.SetInteger("State", 1);
	}


	void flyToPosition()
	{
		if (!goingToPos)
		{
			goingToPos = true;
			//IOS gotoPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			gotoPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			gotoPosition.z = 0;
//			print (gotoPosition);
			startFlyTime = Time.time;
		}

		transform.position = Vector3.Lerp(initPosition, gotoPosition, (Time.time - startFlyTime) / flyDuration);
	}

	void doAction()
	{
		// 0 -> walk
		// 1 -> fly
		// 2 -> punchmid
		switch (animator.GetInteger("State"))
		{
		//walk
		case 0:
			nextState();
			break;
		//fly
		case 1:
			flyToPosition();
			break;
		case 2:

			break;
		

//		default:

		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//IOS if (Input.touchCount > 0)
		if (Input.GetMouseButton(0))
		{
			doAction();
//			animator.SetInteger("State", 1);
//			setGotoPosition();
//
//			transform.position = Vector3.Lerp(initPosition, gotoPosition, (Time.time - startFlyTime) / flyDuration);

//			print (transform.position);
		}

		else
		{
			defaultState();
		}

	}
}