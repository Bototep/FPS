using UnityEngine;

public class BossZone : MonoBehaviour
{
	public GameObject bossHPBar;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			bossHPBar.gameObject.SetActive(true);
		}
	}
}
