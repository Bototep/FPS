using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	private Transform player; 
	private NavMeshAgent agent;
	private ObjectPooler objectPooler;

	public float detectionRange = 10f; 
	private bool isFollowingPlayer = false;

	[System.Obsolete]
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player")?.transform;

		agent = GetComponent<NavMeshAgent>();

		objectPooler = FindObjectOfType<ObjectPooler>();

		if (player != null)
		{
			agent.SetDestination(transform.position);
		}
		else
		{
			Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag.");
		}
	}

	void Update()
	{
		if (player != null)
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.position);

			if (distanceToPlayer <= detectionRange && !isFollowingPlayer)
			{
				isFollowingPlayer = true;
			}

			if (isFollowingPlayer)
			{
				agent.SetDestination(player.position);
			}
		}
	}

	public void ReturnToPool()
	{
		if (objectPooler != null)
		{
			objectPooler.ReturnEnemy(gameObject);
		}
	}
}
