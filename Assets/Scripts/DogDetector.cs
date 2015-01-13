using UnityEngine;
using System.Collections;

public class DogDetector : MonoBehaviour {

	public GameObject DetectedDog;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject != null && other.gameObject.tag == "Dog" && other.isTrigger == false){
			Debug.Log("DOG detector collision!");
			DetectedDog = other.gameObject;
		}
		
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject != null && other.gameObject.tag == "Dog" && other.isTrigger == false){
			Debug.Log("DOG detector - dog left!");
			DetectedDog = null;
		}
	}

}
