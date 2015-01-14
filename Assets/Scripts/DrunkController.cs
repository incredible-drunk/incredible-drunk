using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrunkController : MonoBehaviour {

	public float speed = 1f;
	public float minSecondsBetweenIdle = 4;
	public float maxSecondsBetweenIdle = 10;

	private float nextIdle = 0;
	private List<string> anims = new List<string> ();

	private GameState gameState;

    private Animator anim;

	void Awake() {

		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}

	// Use this for initialization
	void Start () {
		ComputeNextIdle ();
		Random.seed = System.DateTime.Today.Millisecond;
		anims.Add ("Tdrink");
		anims.Add ("Thead");
		anims.Add ("Tbend");
		anims.Add ("Tscratch");

        anim = GetComponent<Animator>();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
            Drink();

        if (Input.GetKeyDown(KeyCode.W))
            Head();

        if (Input.GetKeyDown(KeyCode.E))
            Point();

        if (Input.GetKeyDown(KeyCode.R))
            Bend();

        if (Input.GetKeyDown(KeyCode.T))
            Hand();
       
    }

	private void ComputeNextIdle(){
		nextIdle = Time.time + Random.Range (minSecondsBetweenIdle, maxSecondsBetweenIdle);
	}

	private void PlayIdle(){
		if (Time.time >= nextIdle) {
			if (gameState.State == GameStates.Planning || gameState.State == GameStates.Intro) {
				int random = Random.Range(0,anims.Count);
				
			    string pickedAnimT = anims.ToArray()[random];
				Debug.Log("Playing idle " + pickedAnimT);
				anim.SetTrigger(pickedAnimT);
			}		

			ComputeNextIdle ();
		}
	}
	
	void FixedUpdate () {
		PlayIdle ();
		if (gameState.State == GameStates.Simulation) {
			rigidbody2D.velocity = new Vector2(transform.localScale.x * speed, rigidbody2D.velocity.y);
            Walk();
		}

		if (transform.position.x > gameState.PlayableAreaMaxX) {
			gameState.State = GameStates.GameOverLose;
            Stop();
		}
	}

    private void Drink() {
        anim.SetTrigger("Tdrink");
    }

    private void Head() {
        anim.SetTrigger("Thead");
    }

    private void Point() {
        anim.SetTrigger("Tpoint");
    }

    private void Bend() {
        anim.SetTrigger("Tbend");
    }

    private void Hand()
    {
        anim.SetTrigger("Thand");
    }

    private void Walk()
    {
        anim.SetTrigger("Twalk");
    }

    private void Stop() {
        anim.SetTrigger("Tstop");
    }
}
