using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;

	public float speed = 12f;
	public float gravity = -19.62f;
	public float jumpHeight = 3f;

	public Transform Camera;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	void Update()
	{
		groundCheck.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -5f;
		}

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 move = Camera.transform.right * x + new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z) * z;
		move.Normalize();

		characterController.Move(move * speed * Time.deltaTime);

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		if (isGrounded && velocity.y < -5f)
		{
			velocity.y = -5f;
		}

		characterController.Move(velocity * Time.deltaTime);
	}
}
