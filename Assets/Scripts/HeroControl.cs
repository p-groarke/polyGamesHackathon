using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	public float speed = 0.1F;
	public float flyDuration = 5.0F;
	public float goBackDuration = 0.2f;
	public float fightCounterTime = 3.0f;
	public float singleClickTime = 0.1f;

	bool goingBack;
	bool fighting;

	Animator animator;
	Vector3 initPosition;
	Vector3 gotoPosition;
	bool goingToPos;
	float startFlyTime;
	bool collidingWithEnemy;
	bool gotMouseUp;

	Vector2 swipeFirstPosition;
	Vector2 swipeCurrent;

	// Use this for initialization
	void Start () 
	{
		goingBack = false;
		goingToPos = false;
		fighting = false;

		initPosition = transform.position;
		animator = this.GetComponent<Animator>();
		animator.SetInteger("State", 0);
		collidingWithEnemy = false;

		gotMouseUp = false;

	}
	// ULTIMATE FIGHTING COUNTER
	IEnumerator startFightCounter()
	{
		fighting = true;
		AudioHitHandler.instance.playSound();
		yield return new WaitForSeconds(fightCounterTime);
		goBack();
		fighting = false;
//		print ("Done waiting" + Time.time);

	}

	IEnumerator singleClickCounter()
	{
//		print ("TATA");
		gotMouseUp = false;
		yield return new WaitForSeconds(singleClickTime);
		print (gotMouseUp);
		if (gotMouseUp)
		{
//			print ("so weird");
			punch();
		}
		gotMouseUp = false;
	}


	// COLLIDING
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") == true)
			collidingWithEnemy = true;
	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") == true)
			collidingWithEnemy = false;
	}



	void nextState()
	{
		animator.SetInteger("State", animator.GetInteger("State")+1);
	}


	// GO BACK
	void goBack()
	{
		if (!goingBack)
		{
			goingBack = true;
			goingToPos = false;
			animator.SetInteger("State", 0);
			gotoPosition = transform.position;
			gotoPosition.z = 0;
			//transform.position = initPosition;
			startFlyTime = Time.time;
		}

		transform.position = Vector3.Lerp(gotoPosition, initPosition, (Time.time - startFlyTime) / goBackDuration);

		if (transform.position == initPosition)
			goingBack = false;
	}


	// GO ATTACK
	void flyToPosition()
	{
		if (!goingToPos)
		{
			goingToPos = true;
			goingBack = false;
			animator.SetInteger("State", 1);
			//IOS gotoPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			gotoPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			gotoPosition.z = 0;
//			print (gotoPosition);
			startFlyTime = Time.time;
		}

		transform.position = Vector3.Lerp(initPosition, gotoPosition, (Time.time - startFlyTime) / flyDuration);

		if (transform.position == gotoPosition)
			goingToPos = false;

		if (collidingWithEnemy)
			nextState();
	}

	void punch()
	{
		print ("punch");
		animator.SetInteger("State", 2);
//		animator.Play()
//		if (animator.GetInteger("State") == 2)
//			print ("Punching!");

//		animator.SetInteger("State", 0);
	}

//	void doAction()
//	{
//		// 0 -> walk
//		// 1 -> fly
//		// 2 -> punchmid
//		switch (animator.GetInteger("State"))
//		{
//		//walk
//		case 0:
//			nextState();
//			break;
//		//fly
//		case 1:
//			flyToPosition();
//			break;
//		// punch
//		case 2:
//			punch();
//			break;
//		
//
////		default:
//
//		}
//	}
	
	// Update is called once per frame
	void Update () 
	{
		if (goingBack)
		{
//			print ("goingback");
			goBack();
		}

		else if (goingToPos)
		{
//			print ("goingtopos");
			flyToPosition();
		}

		else
		{
			//SWIPE STUFF
			if (Input.GetMouseButtonDown(0))
			{
				swipeFirstPosition = Input.mousePosition;

				if (!fighting)
				{
					//print ("mouseDOWN");
					StartCoroutine(startFightCounter());
					flyToPosition();
				}

				// We are fighting
				else
				{
					StartCoroutine(singleClickCounter());
					//print ("mousedown");
				}

			

			}
				
			
			// Check swipes
			else if (Input.GetMouseButtonUp(0))
			{
				gotMouseUp = true;

				swipeCurrent = (Vector2)Input.mousePosition - swipeFirstPosition;
				Vector2 swipeNorm = swipeCurrent.normalized;
				
				// Swipe up
				if (-0.7f <= swipeNorm.x && swipeNorm.x < 0.7f
				    && swipeNorm.y > 0 )
					print ("SwipeUp");
				// Swipe Down
				if (-0.7f <= swipeNorm.x && swipeNorm.x < 0.7f
				    && swipeNorm.y < 0)
					print ("SwipeDown");
				// Swipe Left
				if (swipeNorm.x < 0 &&
				    0.7f >= swipeNorm.y && swipeNorm.y > -0.7f)
					print ( "SwipeLeft");
				// Swipe Right
				if (swipeNorm.x > 0 &&
				    0.7f >= swipeNorm.y && swipeNorm.y > -0.7f)
					print ("SwipeRight");
				
				
				//Debug.Log(swipeCurrent+", "+swipeCurrent.normalized);
			}
			
			
//			//IOS if (Input.touchCount > 0)
//			if (Input.GetMouseButton(0))
//			{
//				doAction();
//				//			animator.SetInteger("State", 1);
//				//			setGotoPosition();
//				//
//				//			transform.position = Vector3.Lerp(initPosition, gotoPosition, (Time.time - startFlyTime) / flyDuration);
//				
//				//			print (transform.position);
//			}
			
//			else
//			{
//				goBack();
//			}
		}


	}
}