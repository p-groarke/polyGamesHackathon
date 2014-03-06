using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float deplacement = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")) 
		{
			Vector3 newPosition = enemy.transform.position;
			newPosition.x = enemy.transform.position.x + deplacement;
			if (newPosition.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
				enemy.transform.position = newPosition;
		}
	}
}
