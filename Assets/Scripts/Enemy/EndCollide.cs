using UnityEngine;

public class EndCollide : MonoBehaviour
{
	private CheckpointManager checkpointManager;
	private ObjectPooler objectPooler;

	[System.Obsolete]
	void Start()
	{
		checkpointManager = FindObjectOfType<CheckpointManager>();
		objectPooler = FindObjectOfType<ObjectPooler>();
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
		}
	}
}
