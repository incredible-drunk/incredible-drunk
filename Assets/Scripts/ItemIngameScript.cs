using UnityEngine;
using System.Collections;

public class ItemIngameScript : MonoBehaviour {

	protected GameState gameState;

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
