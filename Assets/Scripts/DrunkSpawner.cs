using UnityEngine;
using System.Collections;

public class DrunkSpawner : MonoBehaviour, IGameStateListener {
	private GameState gameState;
	public GameObject SpawnedDrunk;
	public GameObject DrunkPrefab;

	void Awake(){
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
		gameState.RegisterGameStateListener (this);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IGameStateListener implementation

	public void OnGameStateChange (GameStates oldStates, GameStates newState)
	{
		if(newState == GameStates.Intro || newState == GameStates.Planning){
			RespawnDrunk();
		}
	}

	#endregion

	private void RespawnDrunk(){
		if (SpawnedDrunk != null) {
			Destroy(SpawnedDrunk);
			SpawnedDrunk = null;
		}
		SpawnedDrunk = (GameObject) Instantiate (DrunkPrefab);
		SpawnedDrunk.rigidbody2D.position = new Vector2(this.transform.position.x,this.transform.position.y);
	}
}
