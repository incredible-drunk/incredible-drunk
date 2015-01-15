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

public class GameState : MonoBehaviour, IGameStateListener {
	private bool initialized = false;
	public GameStates State;
	public GameObject GameOverUI;
	public GameObject GameStateTextUi;
	public GameObject GameStateButton;
	public GameObject GameResetButton;
	public Text 	  GameResultText;
	public float PlayableAreaMinY;
	public float PlayableAreaMaxY;
	public float PlayableAreaMinX;
	public float PlayableAreaMaxX;
	public AudioClip IntroMusic;
	public AudioClip GameMusic;

	private List<IGameStateListener> _gameStateListeners = new List<IGameStateListener>();


	// Use this for initialization
	void Start () {
		if (!initialized) {
			SetNewGameState(State);
			initialized = true;
			Random.seed = (int)System.DateTime.Now.Ticks;
		}
	}

	void Awake(){
		this.RegisterGameStateListener (this);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var textComponent = GameStateTextUi.GetComponent<Text> ();
		if (State == GameStates.Intro) {
			GameStateButton.SetActive(false);
			GameResetButton.SetActive(false);
			GameStateTextUi.SetActive(false);
			GameOverUI.SetActive(false);	
		}
		if (State == GameStates.Planning) {
				GameResetButton.SetActive(true);
				GameStateButton.SetActive(true);
				GameStateTextUi.SetActive(true);
				GameOverUI.SetActive(false);	
				textComponent.text = "Plánovací fáze";		
		} else if (State == GameStates.Simulation) {
				GameStateButton.SetActive(false);
				GameResetButton.SetActive(true);
				GameStateTextUi.SetActive(true);
				GameOverUI.SetActive(false);	
				textComponent.text = "Snímací fáze";
		} else if (State == GameStates.Intro || State == GameStates.GameOverLose || State == GameStates.GameOverWin) {
			textComponent.text = "";

		}
	}

	public void StartSimulation(){
		SetNewGameState (GameStates.Simulation);
	}

	public void Reset(){
		SetNewGameState (GameStates.Planning);
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
	
	#region IGameStateListener implementation
	public void OnGameStateChange (GameStates oldStates, GameStates newState)
	{
		if (newState == GameStates.Intro) {
				audio.volume = 1f;
				audio.clip = IntroMusic;
				audio.loop = false;
				audio.Play ();
		} else if (newState == GameStates.Planning && oldStates == GameStates.Intro) {
				audio.volume = 0.22f;
				audio.clip = GameMusic;
				audio.loop = true;
				audio.Play ();
		} else if (newState == GameStates.GameOverLose) {
				GameOverUI.SetActive (true);
				GameResultText.text = "Prohrál jsi!";
				GameStateButton.SetActive(false);
				GameResetButton.SetActive(false);
				GameStateTextUi.SetActive(false);
		} else if (newState == GameStates.GameOverWin) {
				GameOverUI.SetActive(true);	
				GameResultText.text = "Vyhrál jsi!";
				GameStateButton.SetActive(false);
				GameResetButton.SetActive(false);
				GameStateTextUi.SetActive(false);
		}
	}
	#endregion
}

public interface IGameStateListener{
	void OnGameStateChange(GameStates oldStates, GameStates newState);
}
