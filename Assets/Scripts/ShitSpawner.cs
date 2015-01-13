using UnityEngine;
using System.Collections;

public class ShitSpawner : MonoBehaviour, IGameStateListener {

	private GameState gameState;
	public GameObject SpawnedShit;
	public GameObject ShitPrefab;
	
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
		if(newState == GameStates.Intro || (newState == GameStates.Planning && oldStates != GameStates.Intro)){
			DestroyShit();
		}
	}
	
	#endregion
	private void DestroyShit(){
		if (SpawnedShit != null) {
			Destroy(SpawnedShit);
			SpawnedShit = null;
		}
	}


	public void SpawnShitAtPoint(Vector2 shitLocation){
		DestroyShit ();
		SpawnedShit = (GameObject) Instantiate (ShitPrefab);
		SpawnedShit.transform.position = shitLocation;
	}
}
