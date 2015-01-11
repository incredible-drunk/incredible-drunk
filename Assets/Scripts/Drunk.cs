using UnityEngine;
using System.Collections;

public class Drunk : MonoBehaviour {


	public float moveSpeed = 50f;		// The speed the drunk moves at.


	// Use this for initialization
	void Start () {
	
	}
	


	void FixedUpdate(){
		// Set the enemy's velocity to moveSpeed in the x direction.
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
	}


}
