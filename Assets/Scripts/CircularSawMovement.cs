using UnityEngine;
using System.Collections;

public class CircularSawMovement : MonoBehaviour {

	public float wavingSpeed;
	public float sawingSpeed;
	public float waveAngle = 45f;
	public float startAngle = 30f;
	public float startDirection = 1;

	Rigidbody circularSawRB;
	Rigidbody sawRB;
	Vector3 waveVelocity;
	Vector3 sawAngleVelocity;

	void Start () {
		circularSawRB = GetComponent<Rigidbody> ();
		sawRB = transform.FindChild ("Saw").GetComponent<Rigidbody> ();
		waveVelocity = new Vector3 (wavingSpeed, 0, 0);
		sawAngleVelocity = new Vector3 (0, sawingSpeed, 0);
		if (Mathf.Abs (startAngle) > waveAngle) {
			startAngle = startAngle > 0 ? waveAngle : -waveAngle;
		}
		transform.localEulerAngles = Vector3.right * startAngle;
		waveVelocity *= startDirection;
	}

	void OnEnable() {
		transform.localEulerAngles = Vector3.right * startAngle;
	}
	
	void FixedUpdate()
	{
		if (Mathf.Abs (transform.localRotation.x * 100) > (waveAngle - 5)) {
			waveVelocity *= -1;
		}
		var currentWaveVelocity = waveVelocity;
		if (transform.localRotation.x != 0) {			
			currentWaveVelocity = waveVelocity * ((Mathf.Abs (Mathf.Abs (transform.localRotation.x * 100) - waveAngle)) / waveAngle);
		}
		Quaternion vawingDeltaRotation = Quaternion.Euler(currentWaveVelocity * Time.deltaTime);
		circularSawRB.MoveRotation(circularSawRB.rotation * vawingDeltaRotation);

		Quaternion sawDeltaRotation = Quaternion.Euler(sawAngleVelocity * Time.deltaTime);
		sawRB.MoveRotation(sawRB.rotation * sawDeltaRotation);
	}
}
