using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	private Transform player;
	private NavMeshAgent agent;
	private ObjectPooler objectPooler;
	private CheckpointManager checkpointManager;

	public float detectionRange = 10f;
	private bool isFollowingPlayer = false;
	private Vector3 initialPosition; 
	[System.Obsolete]
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player")?.transform;
		checkpointManager = FindObjectOfType<CheckpointManager>();
		agent = GetComponent<NavMeshAgent>();
		objectPooler = FindObjectOfType<ObjectPooler>();

		initialPosition = transform.position;

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

	public void ResetEnemyPosition()
	{
		transform.position = initialPosition;
		agent.Warp(initialPosition); 
		isFollowingPlayer = false;
		agent.SetDestination(transform.position);
	}

	public void ReturnToPool()
	{
		if (objectPooler != null)
		{
			objectPooler.ReturnEnemy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			checkpointManager.RespawnPlayerImmediate();
			ResetEnemyPosition(); 
		}
	}
}
