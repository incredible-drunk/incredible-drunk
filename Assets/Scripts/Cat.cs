using UnityEngine;
using System.Collections;
using System;

public class Cat : ItemIngameScript {
	//private bool launched;
	public GameObject Target;
	private float timeSinceLastJump = -3.1f;
	public float SecondsToRechargeJump = 3;
	private GameObject targetMouse = null;
	public AudioClip JumpSound = null;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void JumpIfCharged(GameObject target,bool away){
		if (Mathf.Abs (Time.time - timeSinceLastJump) > SecondsToRechargeJump) {

			Vector2 vectorToMouse = target.rigidbody2D.position - this.rigidbody2D.position;
			if(Math.Abs(vectorToMouse.x) > 0.1){
				if(vectorToMouse.x < 0 && transform.localScale.x > 0){
					Flip();
				}else if(vectorToMouse.x > 0 && transform.localScale.x < 0){
					Flip ();
				}
			}
			timeSinceLastJump = Time.time;
			float sign = 1;
			if(transform.localScale.x < 0f){
				sign = -1;
			}
			if(away){
				sign = sign * -1;
			}
			this.rigidbody2D.velocity = new Vector2(sign*(float)4.6,(float)10);
			if(JumpSound != null){
				AudioSource.PlayClipAtPoint(JumpSound,this.transform.position);
			}
		}
	}

	void FixedUpdate(){
		if (gameState.State == GameStates.Simulation) {
			var detectedDog = this.GetComponentInChildren<DogDetector>().DetectedDog;
			if(detectedDog != null){
				JumpIfCharged(detectedDog,true);
			}
			else if (targetMouse != null) {
				JumpIfCharged (targetMouse,false);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject != null && other.gameObject.tag == "Mouse" && other.isTrigger == false){
			Debug.Log("Cat jump detector collision!");
			targetMouse = other.gameObject;

		}
		
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Mouse") {
			other.gameObject.GetComponent<Mouse>().Kill();
		}
	}

	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}



}
