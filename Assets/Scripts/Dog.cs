using UnityEngine;
using System.Collections;
using System;

public class Dog : ItemIngameScript {


	public float MoveSpeed = 2f;
	public float SpotCatSpeed = 4f;
	public float MinDistanceToCat = 40f;
	public float BurgerEatingTime = 2;
	GameObject sensedCat;
	BurgerDetector burgerDetector;
	ShitController shitController;
	private bool eatingBurger;


    
    Animator anim;
	// Use this for initialization
	void Start () {
	    anim = transform.Find("Sprite").GetComponent<Animator>();
		burgerDetector = GetComponentInChildren<BurgerDetector> ();
		shitController = GetComponentInChildren<ShitController> ();
	}


	public void Awake(){
		base.Awake ();
	}
	
	// Update is called once per frame
	void Update () {
        
	}


	void FixedUpdate(){
		if (gameState.State == GameStates.Simulation) {
			if(burgerDetector.Burger != null && eatingBurger == false){
				eatingBurger = true;
                var animator = gameObject.GetComponentInChildren<Animator>();
                animator.SetTrigger("eat");
				Destroy(burgerDetector.Burger,BurgerEatingTime);
                burgerDetector.Burger = null;
				Invoke("BurgerEaten",BurgerEatingTime);
			}
			if(eatingBurger == false){
				if(sensedCat == null ){
					BurgerBehaviour();
				}else{
					CatBehavior();
				}
			}else if(shitController.IsAfterShit){
				AfterShitBehavior();
			}
			//rigidbody2D.velocity = new Vector2(transform.localScale.x * MoveSpeed, rigidbody2D.velocity.y);	
		}
        anim.SetFloat("currentVelocity", Mathf.Abs(rigidbody2D.velocity.x));
	}
	public void BurgerEaten(){

		if (this.gameObject.activeSelf && gameState.State == GameStates.Simulation) {
			shitController.takeShit ();
		}
	}
	void AfterShitBehavior(){
        sensedCat = null;
		if(transform.localScale.x < 0) {
			Flip ();
		}
        rigidbody2D.velocity = new Vector2(transform.localScale.x * SpotCatSpeed, rigidbody2D.velocity.y);
	}


	void CatBehavior(){
		Vector2 vectorToCat = sensedCat.rigidbody2D.position - this.rigidbody2D.position;
		if(vectorToCat.magnitude < MinDistanceToCat){
			if(Math.Abs(vectorToCat.x) > 0.2){				
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

	void BurgerBehaviour(){
		GameObject burger =    (GameObject) GameObject.FindGameObjectWithTag("Burger");
		if(burger != null && burger.rigidbody2D != null){
			Vector2 vectorToCheese = burger.rigidbody2D.position - this.rigidbody2D.position;
			if(Math.Abs(vectorToCheese.x) > 0.1){				
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
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

	public void Kill(){
		Destroy (this.gameObject);
	}
}
