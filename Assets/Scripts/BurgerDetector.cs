using UnityEngine;
using System.Collections;

public class BurgerDetector : MonoBehaviour {

	public GameObject Burger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject != null && other.gameObject.tag == "Burger" && other.isTrigger == false){
			Debug.Log("Burger detector collision!");
			Burger = other.gameObject;
		}
		
	}

}
