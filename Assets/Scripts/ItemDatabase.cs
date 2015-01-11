using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public Dictionary<string,Item> Items = new Dictionary<string,Item>();

	// Use this for initialization
	void Start () {
		AddItem(new Item ("CUTTERS","Štípačky","Dokážou uštípnout co chcete."));
		AddItem (new Item ("MOUSE","Myš","Malá, bílá, píská."));
		AddItem (new Item ("POO","Hovno","Opravdu velké hovno"));
	}

	private void AddItem(Item item){
		Items.Add (item.ID, item);
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
