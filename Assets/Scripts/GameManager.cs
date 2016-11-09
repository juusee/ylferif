using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float endDelay = 2.5f;
	public PlayerManager playerManager;
	public GameLogic gameLogic;
	public GameObject panel;
	public GameObject startCounter;

	WaitForSeconds endWait;
	string level = null;

	public void startGame(string level)
	{
		this.level = level;
	}

	void Start ()
	{
		endWait = new WaitForSeconds (endDelay);

		StartCoroutine (GameLoop());
	}

	private IEnumerator GameLoop ()
	{		
		panel.SetActive (true);
		yield return StartCoroutine (RoundStarting());
		panel.SetActive (false);

		startCounter.SetActive (true);
		yield return StartCoroutine (StartCounter (3));
		startCounter.SetActive (false);
		yield return StartCoroutine (RoundPlaying());
		yield return StartCoroutine (RoundEnding());
		StartCoroutine (GameLoop ());
	}

	private IEnumerator RoundStarting ()
	{
		playerManager.Reset ();
		playerManager.DisableControl ();

		while (level == null) {
			yield return null;
		}

		gameLogic.setLevel(int.Parse(level));
		gameLogic.Reset ();

		level = null;
	}

	private IEnumerator StartCounter (int count)
	{
		WaitForSeconds countWait = new WaitForSeconds (0.8f);
		startCounter.GetComponentInChildren<Text> ().text = count.ToString();
		yield return countWait;
		--count;
		if (count > 0) {
			yield return StartCounter (count);
		}
	}

	private IEnumerator RoundPlaying ()
	{
		playerManager.EnableControl ();

		while (playerManager.playerInstance.activeSelf) {
			yield return null;
		}
	}

	private IEnumerator RoundEnding ()
	{
		playerManager.DisableControl ();
		yield return endWait;
	}
}