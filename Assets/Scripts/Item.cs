using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Item  {

	public string Name;
	public string Description;
	public string ID;
	public Sprite Icon;

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

		Debug.Log ("Loaded " + id.ToLower()+"_item");
	}


}
