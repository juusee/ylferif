using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VerticalMovement : MonoBehaviour
{

	public float speed = 50;
	public bool goDown = false;

	Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate ()
	{
		if (goDown) {
			rb.velocity = new Vector3 (0, -speed, 0);
		} else {
			rb.velocity = new Vector3 (0, speed, 0);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		goDown = !goDown;
	}
}
