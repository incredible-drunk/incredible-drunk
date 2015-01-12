using UnityEngine;
using System.Collections;

public enum GameStates{
	Intro,
	Planning,
	Preparation,
	GameOverWin,
	GameOverLose

}

public class GameState : MonoBehaviour {

	public GameStates State;
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
