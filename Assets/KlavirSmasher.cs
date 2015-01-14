using UnityEngine;
using System.Collections;

public class KlavirSmasher : MonoBehaviour {

	bool smashed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !smashed) {
			smashed = true;
			Debug.Log("Hit the ground!");
			GetComponent<Animator>().SetTrigger("Tsmash");
			var source = GetComponent<AudioSource>();
			//source.time = 0.5f;
			source.Play();

		}
	}
}
