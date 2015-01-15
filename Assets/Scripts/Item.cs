using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item  {

	public string Name;
	public string Description;
	public string ID;
	public Sprite Icon;
	public Sprite DragIcon;
	public GameObject InGameObjectPrefab;
	public ItemSoundBank SoundBank;
	protected int protagonistSpeechCounter = 0;
	protected int drunkSpeechCounter = 0;


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
		this.DragIcon = Resources.Load<Sprite> (id.ToLower()+"_drag");
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

	public AudioClip GetNextDrunkClip(){
		if (SoundBank.DrunkCommentary == null || SoundBank.DrunkCommentary.Length == 0) {
			return null;		
		}
		var clip = SoundBank.DrunkCommentary[drunkSpeechCounter%SoundBank.DrunkCommentary.Length];
		Debug.Log ("Retturning clip " + clip.name + " : " + drunkSpeechCounter); 
		drunkSpeechCounter++;
//		if (drunkSpeechCounter < SoundBank.DrunkCommentary.Length-1) {
//			
//			drunkSpeechCounter = drunkSpeechCounter+1;
//		}
		return clip;
	}


}
