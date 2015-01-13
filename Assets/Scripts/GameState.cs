using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum GameStates{
	Intro,
	Planning,
	Simulation,
	GameOverWin,
	GameOverLose

}

public class GameState : MonoBehaviour {

	public GameStates State;
	public GameObject GameStateTextUi;
	public GameObject GameStateButton;
	public float PlayableAreaMinY;
	public float PlayableAreaMaxY;
	public float PlayableAreaMinX;
	public float PlayableAreaMaxX;
	private List<IGameStateListener> _gameStateListeners = new List<IGameStateListener>();


	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
	
	}
	
	// Update is called once per frame
	void Update () {
		var textComponent = GameStateTextUi.GetComponent<Text> ();
		if (State == GameStates.Planning) {
				GameStateButton.SetActive(true);
				textComponent.text = "Plánovací fáze";		
		} else if (State == GameStates.Simulation) {
				GameStateButton.SetActive(false);
				textComponent.text = "Snímací fáze";
		} else if (State == GameStates.Intro || State == GameStates.GameOverLose || State == GameStates.GameOverWin) {
			textComponent.text = "";

		}
	}

	public void StartSimulation(){
		SetNewGameState (GameStates.Simulation);
	}

	public void SetNewGameState(GameStates newGameState){

		GameStates oldGameState = this.State;
		this.State = newGameState;
		foreach (var listener in _gameStateListeners) {
			listener.OnGameStateChange(oldGameState,newGameState);
		}
	}

	public void RegisterGameStateListener(IGameStateListener listener){
		_gameStateListeners.Add (listener);
	}

	public bool IsWithingPlayableArea(Vector3 vec){
		if (vec.x >= PlayableAreaMinX && vec.x <= PlayableAreaMaxX &&
						vec.y >= PlayableAreaMinY && vec.y <= PlayableAreaMaxY) {
						return true;		
				} else {
			return false;		
		}
	}
	
}

public interface IGameStateListener{
	void OnGameStateChange(GameStates oldStates, GameStates newState);
}
