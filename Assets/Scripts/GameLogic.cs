using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {

	public GameObject container;
	public GameObject pile;
	public GameObject movingPile;
	public GameObject rotatingPile;
	public GameObject circularSaw;
	public GameObject guillotine;

	public Transform player;
	public Transform containerSpawnPoint;
	public float containerBufferFront;

	List<GameObject> containers = new List<GameObject> ();
	List<GameObject> piles = new List<GameObject> ();
	List<GameObject> movingPiles = new List<GameObject> ();
	List<GameObject> rotatingPiles = new List<GameObject> ();
	List<GameObject> circularSaws = new List<GameObject> ();
	List<GameObject> guillotines = new List<GameObject> ();

	float containerLength;
	int containerBufferBack = 2;
	int containerCount = 0;
	float levelLength = 1500f;
	int level;

	void Start ()
	{
		containerLength = container.transform.GetChild(0).GetComponent<Renderer> ().bounds.size.x;
	}
	
	void Update ()
	{
		if (player.transform.position.x > levelLength + 50f) {
			player.gameObject.SetActive (false);
		}

		float distanceToSpawnContainer = player.position.x + containerBufferFront * containerLength;
		if (distanceToSpawnContainer < levelLength && containerSpawnPoint.position.x < distanceToSpawnContainer) {
			GameObject container = getContainer ();
			container.transform.position = new Vector3 (
				containerSpawnPoint.position.x,
				container.transform.position.y,
				container.transform.position.z
			);

			if (level == 1) {
				if (containerCount > 0 && containerCount % 2 == 0) {
					GameObject pile = getPile ();
					pile.transform.position = new Vector3 (
						containerSpawnPoint.position.x,
						pile.transform.position.y,
						pile.transform.position.z
					);
					if (containerCount % 4 == 0) {
						pile.transform.Rotate (Vector3.right * 90);
					}
					pile.SetActive (true);
				}
			}

			if (level == 2) {
				if (containerCount > 0 && containerCount % 2 == 0) {
					GameObject pile = null;
					if (containerCount % 4 == 0) {
						pile = getMovingPile ();
					} else {
						pile = getRotatingPile ();
					}
					pile.transform.position = new Vector3 (
						containerSpawnPoint.position.x,
						pile.transform.position.y,
						pile.transform.position.z
					);
					pile.SetActive (true);
				}
			}

			if (level == 3) {
				if (containerCount > 0 && containerCount % 2 == 0) {
					if (containerCount % 4 == 0) {
						GameObject circularSaw = getCircularSaw ();
						circularSaw.transform.position = new Vector3 (
							containerSpawnPoint.position.x,
							circularSaw.transform.position.y,
							circularSaw.transform.position.z
						);
						circularSaw.GetComponent<CircularSawMovement> ().startAngle = -45f;
						circularSaw.SetActive (true);
						GameObject circularSaw2 = getCircularSaw ();
						circularSaw2.transform.position = new Vector3 (
							containerSpawnPoint.position.x + 80f,
							circularSaw2.transform.position.y,
							circularSaw2.transform.position.z
						);
						circularSaw2.GetComponent<CircularSawMovement> ().startAngle = 45f;
						circularSaw2.GetComponent<CircularSawMovement> ().startDirection = -1f;
						circularSaw2.SetActive (true);
					} else {
						GameObject guillotine = getGuillotine ();
						guillotine.transform.position = new Vector3 (
							containerSpawnPoint.position.x,
							guillotine.transform.position.y,
							guillotine.transform.position.z
						);
						guillotine.SetActive (true);					
					}
				}
			}

			container.SetActive (true);
			++containerCount;
			containerSpawnPoint.position = new Vector3 (
				containerSpawnPoint.position.x + containerLength,
				containerSpawnPoint.position.y,
				containerSpawnPoint.position.z
			);
		}
	}

	GameObject getContainer() {
		GameObject previousContainer = null;
		GameObject newContainer = null;
		for (int i = 0; i < containers.Count; ++i) {
			if (!containers [i].activeSelf || containers [i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newContainer = containers [i];
				break;
			} 
		}
		// todo better
		for (int i = 0; i < containers.Count; ++i) {
			if (containers[i].activeSelf && (previousContainer == null || containers [i].transform.position.x > previousContainer.transform.position.x)) {
				previousContainer = containers [i];
			}
		}
		if (newContainer == null) {
			newContainer = (GameObject) Instantiate (container);
			newContainer.transform.name = newContainer.transform.name + containers.Count.ToString();
			containers.Add (newContainer);
		}

		// Todo better place
		if (level == 4) {
			if (previousContainer != null) {
				newContainer.transform.position = previousContainer.GetComponent<ContainerMovement> ().GetTargetPosition ();
				newContainer.transform.rotation = previousContainer.GetComponent<ContainerMovement> ().GetTargetRotation ();
			}
			// Todo better. Do not rotate first container
			if (previousContainer != null) {
				newContainer.GetComponent<ContainerMovement> ().SetNewSpeedAndAngle ();
			}		
		}
		newContainer.SetActive (false);
		return newContainer;
	}

	GameObject getPile() {
		GameObject newPile = null;
		for (int i = 0; i < piles.Count; ++i) {
			if (!piles[i].activeSelf || piles[i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newPile = piles [i];
				break;
			}
		}
		if (newPile == null) {
			newPile = (GameObject) Instantiate (pile);
			piles.Add (newPile);
		}
		newPile.SetActive (false);
		return newPile;
	}

	GameObject getMovingPile() {
		GameObject newMovingPile = null;
		for (int i = 0; i < movingPiles.Count; ++i) {
			if (!movingPiles[i].activeSelf || movingPiles[i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newMovingPile = movingPiles [i];
				break;
			}
		}
		if (newMovingPile == null) {
			newMovingPile = (GameObject) Instantiate (movingPile);
			movingPiles.Add (newMovingPile);
		}
		newMovingPile.SetActive (false);
		return newMovingPile;
	}

	GameObject getRotatingPile() {
		GameObject newRotatingPile = null;
		for (int i = 0; i < rotatingPiles.Count; ++i) {
			if (!rotatingPiles[i].activeSelf || rotatingPiles[i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newRotatingPile = rotatingPiles [i];
				break;
			}
		}
		if (newRotatingPile == null) {
			newRotatingPile = (GameObject) Instantiate (rotatingPile);
			rotatingPiles.Add (newRotatingPile);
		}
		newRotatingPile.SetActive (false);
		return newRotatingPile;
	}

	GameObject getCircularSaw() {
		GameObject newCircularSaw = null;
		for (int i = 0; i < circularSaws.Count; ++i) {
			if (!circularSaws[i].activeSelf || circularSaws[i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newCircularSaw = circularSaws [i];
				break;
			}
		}
		if (newCircularSaw == null) {
			newCircularSaw = (GameObject) Instantiate (circularSaw);
			circularSaws.Add (newCircularSaw);
		}
		newCircularSaw.SetActive (false);
		return newCircularSaw;
	}

	GameObject getGuillotine() {
		GameObject newGuillotine = null;
		for (int i = 0; i < guillotines.Count; ++i) {
			if (!guillotines[i].activeSelf || guillotines[i].transform.position.x < (player.transform.position.x - containerBufferBack * containerLength)) {
				newGuillotine = guillotines [i];
				break;
			}
		}
		if (newGuillotine == null) {
			newGuillotine = (GameObject) Instantiate (guillotine);
			guillotines.Add (newGuillotine);
		}
		newGuillotine.SetActive (false);
		return newGuillotine;
	}

	public void Reset ()
	{
		if (level == 4) {
			ContainerMovement.canMove = true;
			containerBufferFront = 2.5f;
		} else {
			ContainerMovement.canMove = false;
			containerBufferFront = 8f;
		}

		// todo if level is same don't destroy gameobjects
		for (int i = 0; i < containers.Count; ++i) {
			containers [i].transform.position = Vector3.zero;
			containers [i].transform.rotation = Quaternion.identity;
			containers [i].GetComponent<ContainerMovement> ().ResetSpeedAndAngle ();
			containers [i].SetActive (false);
		}
		foreach (GameObject pile in piles) {
			Destroy (pile);
		}
		piles = new List<GameObject> ();
		foreach (GameObject movingPile in movingPiles) {
			Destroy (movingPile);
		}
		movingPiles = new List<GameObject> ();
		foreach (GameObject rotatingPile in rotatingPiles) {
			Destroy (rotatingPile);
		}
		rotatingPiles = new List<GameObject> ();
		foreach (GameObject circularSaw in circularSaws) {
			Destroy (circularSaw);
		}
		circularSaws = new List<GameObject> ();
		foreach (GameObject guillotine in guillotines) {
			Destroy (guillotine);
		}
		guillotines = new List<GameObject> ();
		containerSpawnPoint.transform.position = new Vector3 (65, 0, 0);
		containerCount = 0;
	}

	public void setLevel(int level) {
		this.level = level;
	}
}
