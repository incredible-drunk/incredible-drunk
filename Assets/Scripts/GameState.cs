using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public float PlayableAreaMinY;
	public float PlayableAreaMaxY;
	public float PlayableAreaMinX;
	public float PlayableAreaMaxX;

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
	
	}
	
	// Update is called once per frame
	void Update () {
		var textComponent = GameStateTextUi.GetComponent<Text> ();
		if (State == GameStates.Planning) {
				textComponent.text = "Plánovací fáze";		
		} else if (State == GameStates.Simulation) {
				textComponent.text = "Snímací fáze";
		} else if (State == GameStates.Intro || State == GameStates.GameOverLose || State == GameStates.GameOverWin) {
			textComponent.text = "";

		}
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
