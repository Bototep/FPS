using UnityEngine;

public class Throwable : MonoBehaviour
{
	public enum ObjectType
	{
		FireExhaust,
		Document,
		Box
	}

	public ObjectType objectType;
	public float requiredSpeed = 1f;
	private Rigidbody rb;
	private SoundManager soundManager;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		soundManager = SoundManager.instance;
	}

	[System.Obsolete]
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (rb != null && rb.velocity.magnitude >= requiredSpeed)
			{
				Destroy(collision.gameObject);
			}
		}
		else if (collision.gameObject.CompareTag("Boss"))
		{
			if (rb != null && rb.velocity.magnitude >= requiredSpeed)
			{
				Boss boss = collision.gameObject.GetComponent<Boss>();
				if (boss != null)
				{
					boss.TakeDamage(1);
				}
			}
		}

		void OnCollisionEnter(Collision collision)
		{
			Debug.Log("Collision with: " + collision.gameObject.name);
			Debug.Log("Speed: " + rb.velocity.magnitude);
		}


		switch (objectType)
		{
			case ObjectType.FireExhaust:
				soundManager.PlaySoundEffect(0);
				break;
			case ObjectType.Document:
				soundManager.PlaySoundEffect(1);
				break;
			case ObjectType.Box:
				soundManager.PlaySoundEffect(2);
				break;
			default:
				break;
		}
	}
}