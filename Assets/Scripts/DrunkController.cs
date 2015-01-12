using UnityEngine;
using System.Collections;

public class DrunkController : MonoBehaviour {

	public float speed = 1f;

	private GameState gameState;

	void Awake() {
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState.State == GameStates.Simulation) {
			rigidbody2D.velocity = new Vector2(transform.localScale.x * speed, rigidbody2D.velocity.y);	
		}

		if (transform.position.x > gameState.PlayableAreaMaxX) {
			gameState.State = GameStates.GameOverLose;
		}

	}
}
