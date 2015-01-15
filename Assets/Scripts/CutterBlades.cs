using UnityEngine;
using System.Collections;

public class CutterBlades : MonoBehaviour {


	private HingeJoint2D jointToCut;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("Blades collision!");
		if(other.gameObject != null && other.gameObject.GetComponent<HingeJoint2D>() != null){
			Debug.Log("I have joint to cut!");
			jointToCut = other.gameObject.GetComponent<HingeJoint2D>();
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.GetComponent<HingeJoint2D> () != null) {
			jointToCut = null;	
		}	
	}


	public void CutJoint(){
		if (this.jointToCut != null) {
			this.jointToCut.enabled = false;		
		}
	}




}
