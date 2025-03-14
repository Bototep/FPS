using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
	public Transform[] checkpoints; 
	private int lastCheckpointIndex = -1;
	private Transform player;
	public GameManager gameManager;
	private bool isRespawning = false; 

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player")?.transform;
	}

	public void ReachCheckpoint(int checkpointIndex)
	{
		if (checkpointIndex > lastCheckpointIndex)
		{
			lastCheckpointIndex = checkpointIndex;
			Debug.Log("Checkpoint Reached: " + checkpointIndex);
		}
	}

	public void RespawnPlayer()
	{
		if (lastCheckpointIndex >= 0)
		{
			player.position = checkpoints[lastCheckpointIndex].position;
			Debug.Log("Player respawned at checkpoint: " + lastCheckpointIndex);
			if (gameManager != null)
			{
				gameManager.IncrementRespawnCount();
			}
		}
		else
		{
			Debug.Log("No checkpoint reached yet.");
		}
	}

	public void RespawnPlayerImmediate()
	{
		if (lastCheckpointIndex >= 0)
		{
			StartCoroutine(RespawnDelay());
		}
		else
		{
			Debug.Log("No checkpoint reached yet.");
		}
	}

	private IEnumerator RespawnDelay()
	{
		isRespawning = true;
		CharacterController controller = player.GetComponent<CharacterController>();

		if (controller != null)
		{
			controller.enabled = false; 
		}

		player.position = checkpoints[lastCheckpointIndex].position;
		Debug.Log("Player respawned immediately at checkpoint: " + lastCheckpointIndex);
		if (gameManager != null)
		{
			gameManager.IncrementRespawnCount();
		}

		yield return null;

		if (controller != null)
		{
			controller.enabled = true;
		}

		isRespawning = false;
	}

	public bool IsRespawning()
	{
		return isRespawning;
	}
}
