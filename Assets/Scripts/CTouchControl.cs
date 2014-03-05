using UnityEngine;
using System.Collections;

public class CTouchControl : MonoBehaviour {

	public float speed = 0.1F;
	public GameObject particle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
		transform.position = ray.GetPoint();
		

	}


	//	if (Input.touchCount > 0)
//	{
//		transform.position = Input.GetTouch(0).position;
//			print("kljdfskl");
//		}

//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//			transform.Translate(touchDeltaPosition.x * speed, touchDeltaPosition.y * speed, 0);
//		}


//		int i = 0;
//		while (i < Input.touchCount) {
//			if (Input.GetTouch(i).phase == TouchPhase.Began) {
//				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
//				if (Physics.Raycast(ray))
//					Instantiate(particle, transform.position, transform.rotation);// as GameObject;
//				
//			}
//			++i;
//		}

	}
}
