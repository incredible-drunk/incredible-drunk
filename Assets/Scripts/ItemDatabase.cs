using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {



	public Dictionary<string,Item> Items = new Dictionary<string,Item>();

	void Awake(){
		AddItem(new Item  ("CUTTERS","Štípačky","Dokážou uštípnout co chcete."));
		AddItem (new Item ("MOUSE","Myš","Malá, bílá, píská."));
		AddItem (new Item ("POO","Hovno","Opravdu velké hovno"));
		AddItem (new Item ("CHEESE","Sýr","Sýr. Má díry a maj ho rádi myši, a tak."));
		AddItem (new Item ("CAT","Kočka","Nasraná a tlustá kočka."));	
		AddItem (new Item ("BURGER","Zkažený Burger","Po dvou týdnech na čerstvém vzduchu je zapomenutá okurka to poslední, čím je tento burger zkažený."));	
		AddItem (new Item ("DOG","čokl","Co sežere, to hned vysere."));
	}


	// Use this for initialization
	void Start () {

	}

	private void AddItem(Item item){
		Items.Add (item.ID, item);
	
	}
	// Update is called once per frame
	void Update () {
	
	}
}
