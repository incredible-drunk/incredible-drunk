using UnityEngine;
using System.Collections;

public class ChainSpawner : MonoBehaviour, IGameStateListener {
	private GameState gameState;
	public GameObject SpawnedChain;
	public GameObject ChainPrefab;

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
			RespawnChain();
		}
	}

	#endregion

	private void RespawnChain(){
		if (SpawnedChain != null) {
			Destroy(SpawnedChain);
			SpawnedChain = null;
		}
		SpawnedChain = (GameObject) Instantiate (ChainPrefab);
	}
}
