using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	Rigidbody playerRB;
	public float xVelocity;
	public float yVelocity;
	public float zVelocity;

	void Awake ()
	{
		playerRB = GetComponent<Rigidbody>();
		playerRB.velocity = new Vector3 (10f, 0f, 0f);
	}

	void FixedUpdate ()
	{
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		playerRB.velocity = new Vector3 (xVelocity, yVelocity * moveVertical, -zVelocity * moveHorizontal);
	}

	void OnCollisionEnter (Collision col)
	{
		gameObject.SetActive (false);
	}

	void OnEnable ()
	{
		playerRB.isKinematic = false;
	}

	void OnDisable ()
	{
		playerRB.isKinematic = true;
	}
}
