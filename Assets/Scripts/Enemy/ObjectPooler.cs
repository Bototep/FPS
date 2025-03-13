using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
	public GameObject enemyPrefab; 
	public int poolSize = 10;
	private List<GameObject> enemyPool;

	void Start()
	{
		enemyPool = new List<GameObject>();

		for (int i = 0; i < poolSize; i++)
		{
			GameObject enemy = Instantiate(enemyPrefab);
			enemy.SetActive(false); 
			enemyPool.Add(enemy);
		}
	}

	public GameObject GetEnemy()
	{
		foreach (GameObject enemy in enemyPool)
		{
			if (!enemy.activeInHierarchy)
			{
				enemy.SetActive(true); 
				return enemy;
			}
		}

		GameObject newEnemy = Instantiate(enemyPrefab);
		newEnemy.SetActive(true);
		enemyPool.Add(newEnemy);
		return newEnemy;
	}

	public void ReturnEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
	}
}
