using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; // Add for UI elements

public class Boss : MonoBehaviour
{
	private Transform player;
	private NavMeshAgent agent;
	private ObjectPooler objectPooler;
	private CheckpointManager checkpointManager;

	public float detectionRange = 15f;
	private bool isFollowingPlayer = false;
	private Vector3 initialPosition;

	public int maxHP = 10;
	private int currentHP;

	public Image healthBar;

	[System.Obsolete]
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player")?.transform;
		checkpointManager = FindObjectOfType<CheckpointManager>();
		agent = GetComponent<NavMeshAgent>();
		objectPooler = FindObjectOfType<ObjectPooler>();

		initialPosition = transform.position;
		currentHP = maxHP; 

		if (player != null)
		{
			agent.SetDestination(transform.position);
		}
		else
		{
			Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag.");
		}

		UpdateHealthBar();
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

	public void ResetBossPosition()
	{
		transform.position = initialPosition;
		agent.Warp(initialPosition);
		isFollowingPlayer = false;
		agent.SetDestination(transform.position);
	}

	public void TakeDamage(int damage)
	{
		if (isFollowingPlayer)
		{
			currentHP -= damage;
			Debug.Log("Boss HP: " + currentHP);
			UpdateHealthBar();

			if (currentHP <= 0)
			{
				Die();
			}
		}
	}

	private void UpdateHealthBar()
	{
		if (healthBar != null)
		{
			healthBar.fillAmount = (float)currentHP / maxHP; 
		}
	}

	private void Die()
	{
		Debug.Log("Boss Defeated!");
		Destroy(gameObject); 
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			checkpointManager.RespawnPlayerImmediate();
			ResetBossPosition();
		}
	}
}
