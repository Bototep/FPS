using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int checkpointIndex; 
	private CheckpointManager checkpointManager;

	[System.Obsolete]
	void Start()
	{
		checkpointManager = FindObjectOfType<CheckpointManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) 
		{
			checkpointManager.ReachCheckpoint(checkpointIndex);
		}
	}
}
