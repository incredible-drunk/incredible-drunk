using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSoundBank : MonoBehaviour {
	public AudioClip[] ProtagonistCommentary;
	public AudioClip DrunkCommentary;
	public AudioClip SpawnClip;
	protected int protagonistCommentaryCount = 0;

	void Awake(){
		protagonistCommentaryCount = 0;
	}

	// Use this for initialization
	void Start () {
		protagonistCommentaryCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
