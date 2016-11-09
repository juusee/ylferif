using UnityEngine;
using System.Collections;

public class MovingBlockMovement : MonoBehaviour {

	public float speed = 50;

	bool goDown = false;
	Rigidbody blockRB;

	void Start ()
	{
		blockRB = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate ()
	{
		if (goDown) {
			blockRB.velocity = new Vector3 (0, -speed, 0);
		} else {
			blockRB.velocity = new Vector3 (0, speed, 0);
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "ContainerTop") {
			goDown = true;
		}
		if (col.gameObject.tag == "ContainerBottom") {
			goDown = false;
		}
	}
}
