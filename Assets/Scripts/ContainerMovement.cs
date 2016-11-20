using UnityEngine;
using System.Collections;

public class ContainerMovement : MonoBehaviour {

	public static bool canMove = false;

	public float targetAngle;
	public float speed;

	Vector3 origin;
	Vector3 axis;
	Vector3 angleVelocity;
	Rigidbody containerRB;

	Quaternion startAngle;
	Quaternion targetRotation;
	Vector3 targetPosition;

	void Awake()
	{		
		containerRB = GetComponent<Rigidbody>();
		axis = new Vector3 (1, 0, 0);
	}

	void OnEnable()
	{		
		startAngle = transform.rotation;

		float halfContainerWidth = 50f;
		float y = transform.localPosition.y;
		float z = transform.localPosition.z;
		y += Random.value < 0.5 ? halfContainerWidth : -halfContainerWidth;
		z += Random.value < 0.5 ? halfContainerWidth : -halfContainerWidth;
		origin = new Vector3 (transform.localPosition.x, y, z);
		//origin = new Vector3 (transform.localPosition.x, transform.localPosition.y + 50, transform.localPosition.z + 50);

		float angle = targetAngle;
		angle *= speed > 0 ? 1 : -1;
		Quaternion q = Quaternion.AngleAxis (angle, axis);
		targetPosition = q * (containerRB.transform.localPosition - origin) + origin;
		targetRotation = containerRB.transform.localRotation * q;

		print (transform.name + " " + transform.position + " " + transform.rotation);
		print (transform.name + " " + targetPosition + " " + targetRotation);
	}

	void FixedUpdate()
	{			
		if (canMove && Quaternion.Angle(startAngle, transform.rotation) < targetAngle) {
			Quaternion q = Quaternion.AngleAxis (speed, axis);
			containerRB.MovePosition (q * (containerRB.transform.localPosition - origin) + origin);
			containerRB.MoveRotation (containerRB.transform.localRotation * q);
		}
	}

	public void setNewSpeedAndAngle() {
		targetAngle = Random.Range (50, 60);
		speed = Random.value < 0.5 ? 0.5f : -0.5f;
	}

	public Quaternion getTargetRotation() {
		return targetRotation;
	}

	public Vector3 getTargetPosition() {
		return targetPosition;
	}
}
