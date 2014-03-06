using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour {

	public float speed = 0.1F;
	public float flyDuration = 5.0F;
	public float goBackDuration = 0.2f;
	public float fightCounterTime = 3.0f;
	public float singleClickTime = 0.1f;
	public float bonusFightAddition = 0.2f;
	public float bonusFightTime = 0.0f;
	public int HP = 5;

	bool goingBack;
	bool fighting;
	
	bool zoom;

	Animator animator;
	Vector3 initPosition;
	Vector3 gotoPosition;
	bool goingToPos;
	float startFlyTime;
	bool collidingWithEnemy;
	bool gotMouseUp;
	int mouseUpTest;


	Vector2 swipeFirstPosition;
	Vector2 swipeCurrent;

	ZoomMain zoomMain;

	GameObject currentCollidingObject;
	GameObject currentTarget;

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
		mouseUpTest = 0;
		zoom = false;
		zoomMain = Camera.main.GetComponent<ZoomMain>();

		bonusFightTime = 0;

	}

	// ULTIMATE FIGHTING COUNTER
	IEnumerator startFightCounter()
	{
		fighting = true;
		yield return new WaitForSeconds(fightCounterTime);
		while (bonusFightTime > 0.0f)
		{
			yield return new WaitForSeconds(bonusFightTime);
			bonusFightTime = 0;
		}

		goBack();
		fighting = false;
//		print ("Done waiting" + Time.time);

	}

	IEnumerator singleClickCounter()
	{
//		print ("TATA");
		//gotMouseUp = false;
		yield return new WaitForSeconds(singleClickTime);
//		print (gotMouseUp);
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
		{
			collidingWithEnemy = true;
			currentCollidingObject = collision.collider2D.gameObject;
		}

	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy") == true)
			collidingWithEnemy = false;
	}
	

	// GO BACK
	void goBack()
	{
		if (!goingBack)
		{
			zoomMain.zoomPlease = false;
			goingBack = true;
			goingToPos = false;
			animator.SetInteger("State", 1);
			gotoPosition = transform.position;
			gotoPosition.z = 0;
			//transform.position = initPosition;
			startFlyTime = Time.time;
		}

		transform.position = Vector3.Lerp(gotoPosition, initPosition, (Time.time - startFlyTime) / goBackDuration);

		if (transform.position == initPosition)
		{
			animator.SetInteger("State", 0);
			goingBack = false;
		}
	}


	// GO ATTACK
	void flyToPosition()
	{
		if (!goingToPos)
		{
			zoom = true;
			zoomMain.zoomPlease = true;
			goingToPos = true;
			goingBack = false;
			animator.SetInteger("State", 1);
			//gotoPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			//IOS gotoPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			gotoPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			gotoPosition.x = currentTarget.collider2D.transform.position.x - (this.GetComponent<SpriteRenderer>().bounds.size.x / 2);
			gotoPosition.z = 0;
//			print (gotoPosition);
			startFlyTime = Time.time;
		}

		transform.position = Vector3.Lerp(initPosition, gotoPosition, (Time.time - startFlyTime) / flyDuration);

		if (transform.position == gotoPosition)
			goingToPos = false;
	}


	// ATTACKS
	void punch()
	{
		AudioHitHandler.instance.playSound();
		print ("punch");
//		animator.SetInteger("State", 2);
		animator.SetTrigger("Punch");

//		if (currentCollidingObject.gameObject.CompareTag)
		if(currentCollidingObject != null)
		{
			currentCollidingObject.gameObject.GetComponent<Enemy>().Damage(1);
			bonusFightTime += bonusFightAddition;
		}

	}


	void swipeTop()
	{
//		currentCollidingObject
		animator.SetTrigger("SwipeTop");
		if(currentCollidingObject != null)
			currentCollidingObject.gameObject.GetComponent<Enemy>().Damage(1);
	}

	void swipeDown()
	{
		animator.SetTrigger("SwipeDown");
		if(currentCollidingObject != null)
			currentCollidingObject.gameObject.GetComponent<Enemy>().Damage(1);
	}

	void swipeLeft()
	{
		animator.SetTrigger("SwipeLeft");
		if(currentCollidingObject != null)
			currentCollidingObject.gameObject.GetComponent<Enemy>().Damage(1);
	}

	void swipeRight()
	{
		animator.SetTrigger("SwipeRight");
		if(currentCollidingObject != null)
			currentCollidingObject.gameObject.GetComponent<Enemy>().Damage(1);
	}



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
			//if (Input.touchCount > 0)
			if (Input.GetMouseButtonDown(0))
			{
				//swipeFirstPosition = Input.GetTouch(0).position;

				swipeFirstPosition = Input.mousePosition;

				if (!fighting)
				{
					//print ("mouseDOWN");
					//RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
					RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
					
					if(hit && hit.collider.gameObject.CompareTag("Enemy") == true)
					{
						print (hit.transform.tag);
						currentTarget = hit.transform.gameObject;
					}
					else
						return;

					StartCoroutine(startFightCounter());
					flyToPosition();
				}

				// We are fighting
				else
				{
//					animator.SetInteger("State", 1);
					StartCoroutine(singleClickCounter());
					//print ("mousedown");
				}

			

			}
				
			
			// Check swipes
			//else if (Input.touchCount > 0)
			else if (Input.GetMouseButtonUp(0))
			{
				gotMouseUp = true;

				swipeCurrent = (Vector2)Input.mousePosition - swipeFirstPosition;
				//swipeCurrent = (Vector2)Input.GetTouch(0).position - swipeFirstPosition;
				Vector2 swipeNorm = swipeCurrent.normalized;
				
				// Swipe up
				if (-0.7f <= swipeNorm.x && swipeNorm.x < 0.7f
				    && swipeNorm.y > 0 )
				{
//					print ("SwipeUp");
					swipeTop();
				}

				// Swipe Down
				if (-0.7f <= swipeNorm.x && swipeNorm.x < 0.7f
				    && swipeNorm.y < 0)
				{
//					print ("SwipeDown");
					swipeDown();
				}

				// Swipe Left
				if (swipeNorm.x < 0 &&
				    0.7f >= swipeNorm.y && swipeNorm.y > -0.7f)
				{
//					print ( "SwipeLeft");
					swipeLeft();
				}
					
				// Swipe Right
				if (swipeNorm.x > 0 &&
				    0.7f >= swipeNorm.y && swipeNorm.y > -0.7f)
				{
//					print ("SwipeRight");
					swipeRight();
				}
				
				
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