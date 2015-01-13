using UnityEngine;
using System.Collections;

public class DrunkController : MonoBehaviour {

	public float speed = 1f;

	private GameState gameState;

    private Animator anim;

	void Awake() {
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}

	// Use this for initialization
	void Start () {
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
	
	
	void FixedUpdate () {
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
