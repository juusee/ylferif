using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float endDelay = 2.5f;
	public PlayerManager playerManager;
	public GameLogic gameLogic;
	public GameObject panel;
	public GameObject startCounter;

	public Material nightSkybox;
	public Material daySkybox;
	public Cubemap nightCubemap;
	public Cubemap dayCubemap;

	WaitForSeconds endWait;
	string level = null;
	bool isNight = false;

	public void startGame(string level)
	{
		this.level = level;
	}

	public void ChangeNight(bool isNight)
	{
		this.isNight = isNight;
	}

	void Start ()
	{
		endWait = new WaitForSeconds (endDelay);

		StartCoroutine (GameLoop());
	}

	IEnumerator GameLoop ()
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

	IEnumerator RoundStarting ()
	{
		playerManager.DisableControl ();
		playerManager.Reset ();

		while (level == null) {
			yield return null;
		}

		if (this.isNight) {
			RenderSettings.skybox = nightSkybox;
			RenderSettings.customReflection = nightCubemap;
			//RenderSettings.skybox.SetFloat ("_Exposure", 0.5f);
			//RenderSettings.ambientIntensity = 0f;
		} else {
			RenderSettings.skybox = daySkybox;
			RenderSettings.customReflection = dayCubemap;
			//RenderSettings.skybox.SetFloat ("_Exposure", 4f);
			//RenderSettings.ambientIntensity = 1f;
		}

		DynamicGI.UpdateEnvironment ();

		gameLogic.setLevel(int.Parse(level));
		gameLogic.Reset ();
		gameLogic.GetComponent<GameLogic> ().enabled = true;

		level = null;
	}

	IEnumerator StartCounter (int count)
	{
		DynamicGI.UpdateEnvironment ();
		WaitForSeconds countWait = new WaitForSeconds (0.8f);
		startCounter.GetComponentInChildren<Text> ().text = count.ToString();
		yield return countWait;
		--count;
		if (count > 0) {
			yield return StartCounter (count);
		}
	}

	IEnumerator RoundPlaying ()
	{
		playerManager.EnableControl ();

		while (playerManager.playerInstance.activeSelf) {
			yield return null;
		}
	}

	IEnumerator RoundEnding ()
	{
		playerManager.DisableControl ();
		yield return endWait;
	}
}