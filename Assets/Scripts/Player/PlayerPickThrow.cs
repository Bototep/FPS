using UnityEngine;

public class PlayerPickThrow : MonoBehaviour
{
	public Transform hand; 
	public float pickupRange = 2f; 
	public float throwForce = 10f; 
	public LayerMask pickupMask;
	private GameObject heldItem;
	private Rigidbody heldItemRb;
	private SoundManager soundManager;

	private void Start()
	{
		soundManager = SoundManager.instance;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (heldItem == null)
				TryPickUp();
			else
				DropItem();
		}

		if (Input.GetButtonDown("Fire1") && heldItem != null)
		{
			ThrowItem();
		}

		if (heldItem != null)
		{
			heldItem.transform.position = hand.position;
			heldItem.transform.rotation = hand.rotation;
		}
	}

	void TryPickUp()
	{
		Collider[] items = Physics.OverlapSphere(transform.position, pickupRange, pickupMask);

		if (items.Length > 0)
		{
			heldItem = items[0].gameObject;
			heldItemRb = heldItem.GetComponent<Rigidbody>();

			if (heldItemRb != null)
			{
				heldItemRb.isKinematic = true;
				heldItem.transform.SetParent(hand);
			}
		}
	}

	void DropItem()
	{
		if (heldItemRb != null)
		{
			heldItemRb.isKinematic = false;
			heldItem.transform.SetParent(null);
		}
		heldItem = null;
	}

	void ThrowItem()
	{
		soundManager.PlaySoundEffect(3);

		if (heldItemRb != null)
		{
			heldItemRb.isKinematic = false;
			heldItemRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
			heldItem.transform.SetParent(null);

			if (!heldItem.GetComponent<Throwable>())
			{
				heldItem.AddComponent<Throwable>();
			}
		}
		heldItem = null;
	}
}
