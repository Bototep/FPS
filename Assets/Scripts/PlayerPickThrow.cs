using UnityEngine;

public class PlayerPickThrow : MonoBehaviour
{
	public Transform hand; // Where the object will be held
	public float pickupRange = 2f; // How far the player can pick up an item
	public float throwForce = 10f; // Force applied when throwing
	public LayerMask pickupMask; // Define pickupable objects
	private GameObject heldItem;
	private Rigidbody heldItemRb;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
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

		// Make the held item follow the hand position
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
		if (heldItemRb != null)
		{
			heldItemRb.isKinematic = false;
			heldItemRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
			heldItem.transform.SetParent(null);
		}
		heldItem = null;
	}
}
