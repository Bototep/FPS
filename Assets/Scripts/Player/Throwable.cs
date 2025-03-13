using UnityEngine;

public class Throwable : MonoBehaviour
{
	public float requiredSpeed = 5f; 
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			if (rb != null && rb.linearVelocity.magnitude >= requiredSpeed)
			{
				Destroy(collision.gameObject);
			}
		}
	}
}
