using UnityEngine;
using System.Collections;
using System;

public class Mouse : MonoBehaviour {


	public float MoveSpeed = 2000f;
	GameState gameState;

	// Use this for initialization
	void Start () {
	
	}


	public void Awake(){
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate(){
		if (gameState.State == GameStates.Simulation) {
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
			//rigidbody2D.velocity = new Vector2(transform.localScale.x * MoveSpeed, rigidbody2D.velocity.y);	
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
