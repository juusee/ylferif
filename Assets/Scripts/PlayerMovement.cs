using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	Rigidbody playerRB;
	public float xVelocity;
	public float moveVelocity;

	bool inverse = false;

	void Awake ()
	{
		playerRB = GetComponent<Rigidbody>();
		playerRB.velocity = new Vector3 (10f, 0f, 0f);
	}

	void FixedUpdate ()
	{
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		if (inverse) {
			moveVertical *= -1;
		}

		playerRB.velocity = new Vector3 (xVelocity, moveVelocity * moveVertical, -moveVelocity * moveHorizontal);
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

	public void setInverse(bool inverse)
	{
		this.inverse = inverse;
	}

	public void setVelocity(Slider slider)
	{
		this.moveVelocity = slider.value;
	}
}
