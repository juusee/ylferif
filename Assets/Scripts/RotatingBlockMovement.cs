using UnityEngine;
using System.Collections;

public class RotatingBlockMovement : MonoBehaviour {

	public float speedX;
	public float speedY;
	public float speedZ;

	Vector3 angleVelocity;
	Rigidbody rotatingBlockRB;

	void Start()
	{
		rotatingBlockRB = GetComponent<Rigidbody>();
		angleVelocity = new Vector3 (speedX, speedY, speedZ);
	}

	void FixedUpdate()
	{
		Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime);
		rotatingBlockRB.MoveRotation(rotatingBlockRB.rotation * deltaRotation);
	}
}
