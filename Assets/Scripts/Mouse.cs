using UnityEngine;
using System.Collections;
using System;

public class Mouse : ItemIngameScript {


	public float MoveSpeed = 2f;
	public float SpotCatSpeed = 4f;
	public float MinDistanceToCat = 40f;
	GameObject sensedCat;

	// Use this for initialization
	void Start () {
	
	}


	public void Awake(){
		base.Awake ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate(){
		if (gameState.State == GameStates.Simulation) {
			if(sensedCat == null){
				CheeseBehavior();
			}else{
				CatBehavior();
			}
			//rigidbody2D.velocity = new Vector2(transform.localScale.x * MoveSpeed, rigidbody2D.velocity.y);	
		}
	}

	void CatBehavior(){
		Vector2 vectorToCat = sensedCat.rigidbody2D.position - this.rigidbody2D.position;
		if(vectorToCat.magnitude < MinDistanceToCat){
			if(Math.Abs(vectorToCat.x) > 0.2){
				float moveSpeed = MoveSpeed*1;
				if(vectorToCat.x < 0 && transform.localScale.x < 0){
					Flip();
				}else if(vectorToCat.x > 0 && transform.localScale.x > 0){
					Flip ();
				}
				rigidbody2D.velocity = new Vector2(transform.localScale.x * SpotCatSpeed, rigidbody2D.velocity.y);
			}
		}else {//if(vectorToCat.magnitude > MinDistanceToCat + 10){
			sensedCat = null;
		}
	}

	void CheeseBehavior(){
		GameObject cheese =    (GameObject) GameObject.FindGameObjectWithTag("Cheese");
		if(cheese != null && cheese.rigidbody2D != null){
			Vector2 vectorToCheese = cheese.rigidbody2D.position - this.rigidbody2D.position;
			if(Math.Abs(vectorToCheese.x) > 0.2){
				float moveSpeed = MoveSpeed*1;
				if(vectorToCheese.x < 0 && transform.localScale.x > 0){
					Flip();
				}else if(vectorToCheese.x > 0 && transform.localScale.x < 0){
					Flip ();
				}
				rigidbody2D.velocity = new Vector2(transform.localScale.x * MoveSpeed, rigidbody2D.velocity.y);	
			}else{
				rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);	
			}
			
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject != null && other.gameObject.tag == "Cat" && other.isTrigger == false){
			Debug.Log("Sensed Cat!");
			sensedCat = other.gameObject;
			
		}
		
	}

	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}

	public void Kill(){
		Destroy (this.gameObject);
	}
}
