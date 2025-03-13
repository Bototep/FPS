using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	private CheckpointManager checkpointManager;

	public float speed = 12f;
	public float gravity = -19.62f;
	public float jumpHeight = 3f;

	public Transform Camera;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;
	Vector3 move;

	[System.Obsolete]
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		checkpointManager = FindObjectOfType<CheckpointManager>();
	}

	void Update()
	{
		if (checkpointManager.IsRespawning())
		{
			return; 
		}

		groundCheck.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -5f;
		}

		float x = Input.GetAxisRaw("Horizontal"); 
		float z = Input.GetAxisRaw("Vertical");

		if (x != 0 || z != 0)
		{
			move = Camera.transform.right * x + new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z) * z;
			move.Normalize();
		}
		else
		{
			move = Vector3.zero; 
		}

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
