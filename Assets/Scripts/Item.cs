using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item  {

	public string Name;
	public string Description;
	public string ID;
	public Sprite Icon;
	public GameObject InGameObjectPrefab;
	public ItemSoundBank SoundBank;
	protected int protagonistSpeechCounter = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public Item(string id, string name,  string description){
		this.Name = name;
		this.ID = id;
		this.Description = description;
		this.Icon = Resources.Load<Sprite> (id.ToLower()+"_item");
		this.InGameObjectPrefab = Resources.Load<GameObject> (id.ToLower()+"_ingame");
		if (this.InGameObjectPrefab != null) {
			SoundBank = this.InGameObjectPrefab.GetComponent<ItemSoundBank>();
		}

		Debug.Log ("Loaded " + id.ToLower()+"_ingame");
	}

	public AudioClip GetNextProtagonistClip(){
		var clip = SoundBank.ProtagonistCommentary[protagonistSpeechCounter];
		Debug.Log ("Retturning clip " + clip.name + " : " + protagonistSpeechCounter); 
		if (protagonistSpeechCounter < SoundBank.ProtagonistCommentary.Length-1) {
			
			protagonistSpeechCounter = protagonistSpeechCounter+1;
		}
		return clip;
	}


}
