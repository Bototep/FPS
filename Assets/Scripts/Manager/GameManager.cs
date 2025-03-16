using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public GameObject boss;
	public GameObject finish;
	public Button restart;
	public TMP_Text diedTxt;
	public int respawnCount = 0;

	void Start()
	{
		finish.SetActive(false); 
		restart.onClick.AddListener(RestartGame); 

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Time.timeScale = 1f;
	}

	void Update()
	{
		if (boss == null)
		{
			GameOver();
		}
	}

	void GameOver()
	{
		finish.SetActive(true); 
		Time.timeScale = 0f;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void RestartGame()
	{
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void IncrementRespawnCount()
	{
		respawnCount++;
		diedTxt.text = "DIED : " + respawnCount.ToString();
	}
}
