using UnityEngine;
using System.Collections;

public class HeroIngame : MonoBehaviour , IGameStateListener {

	void Awake(){
		GameObject.Find ("GameState").GetComponent<GameState> ().RegisterGameStateListener (this);
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
		if(newState == GameStates.Intro){
			Debug.Log("Starting anim for ingame hero");
			GetComponent<Animator>().SetTrigger("IntroBeginT");
		}
	}

	public void IntroEnded(){
		GameObject.Find ("GameState").GetComponent<GameState> ().SetNewGameState (GameStates.Planning);
	}

	#endregion
}
