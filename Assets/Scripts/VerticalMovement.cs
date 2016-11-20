using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable] 
public class CollisionTagsAndDirections
{
	public string tag;
	public bool goDown;
}

public class VerticalMovement : MonoBehaviour
{

	public float speed = 50;
	public bool goDown = false;
	public CollisionTagsAndDirections[] collisionGameObjectTags = new CollisionTagsAndDirections[1];

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
		for (int i = 0; i < collisionGameObjectTags.Length; ++i) {
			if (collisionGameObjectTags[i].tag == col.gameObject.tag) {
				goDown = collisionGameObjectTags[i].goDown;
				print (transform.name + " "  + col.transform.name + " " + goDown);
			}
		}
	}
}
