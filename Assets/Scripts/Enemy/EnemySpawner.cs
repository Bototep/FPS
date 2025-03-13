using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public ObjectPooler objectPooler;  
	public float spawnInterval = 3f;  
	private float timeSinceLastSpawn = 0f;
	private Transform player;         

	void Start()
	{	
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;

		if (timeSinceLastSpawn >= spawnInterval)
		{
			SpawnEnemy();
			timeSinceLastSpawn = 0f;
		}
	}

	void SpawnEnemy()
	{
		GameObject enemy = objectPooler.GetEnemy();
		enemy.transform.position = transform.position; 

		Enemy enemyScript = enemy.GetComponent<Enemy>();
	}
}
