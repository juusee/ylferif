using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject playerInstance;

	Vector3 playerSpawnPoint;
	PlayerMovement playerMovement;

	void OnEnable() {
		playerSpawnPoint = playerInstance.transform.position;
		playerMovement = playerInstance.GetComponent<PlayerMovement> ();
	
	}

	public void Reset()
	{
		playerInstance.transform.position = playerSpawnPoint;
		playerInstance.SetActive(false);
		playerInstance.SetActive(true);
	}

	public void DisableControl()
	{
		playerMovement.enabled = false;
	}

	public void EnableControl()
	{
		playerMovement.enabled = true;
	}
}