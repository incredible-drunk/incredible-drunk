using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroController : MonoBehaviour, IGameStateListener {

	GameState gameState;

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
		if (newState == GameStates.Intro) {
			enableImages(true);
			GetComponent<Animator> ().SetTrigger ("StartAnim");
		} else if (newState == GameStates.Planning || newState == GameStates.Simulation) {
			enableImages(false);
		}
	}

	#endregion


	private void enableImages(bool enabled){
		var images =  GetComponents<Image>();
		foreach (Image image in images) {
			image.enabled = enabled;
		}
	}

	public void SlidedDown(){
		if (gameState.State == GameStates.Intro) {
			gameState.SetNewGameState(GameStates.Planning);
		}
	
	}
}
