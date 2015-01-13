using UnityEngine;
using System.Collections;

public class ItemIngameScript : MonoBehaviour {

	protected GameState gameState;
	public Vector3 InitialPosition;

	// Use this for initialization
	void Start () {
	
	}

	protected void Awake(){
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
