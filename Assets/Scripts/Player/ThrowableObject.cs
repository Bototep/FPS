using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
	public float throwForce = 10f; 
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		if (rb == null)
		{
			Debug.LogError("No Rigidbody component found on the throwable object.");
		}

		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}

	public void Throw(Vector3 throwDirection)
	{
		rb.isKinematic = false;  
		rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider != null)
		{
			rb.linearVelocity = Vector3.zero; 
			rb.angularVelocity = Vector3.zero;  
		}
	}
}
