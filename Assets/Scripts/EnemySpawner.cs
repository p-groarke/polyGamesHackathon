using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	//private float spawnTime = 1f;		// The amount of time between each spawn.
	public float spawnDelay = 1f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	
	
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, Random.Range (1, 6));
	}
	
	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
	}
}

