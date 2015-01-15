using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {



	public Dictionary<string,Item> Items = new Dictionary<string,Item>();

	void Awake(){
		AddItem(new Item  ("CUTTERS","Štípačky","Kleště značky Uštípnu Cokoli."));
		AddItem (new Item ("MOUSE","Myš","Pravá česká modrooká."));
		AddItem (new Item ("POO","Hovno","Opravdu velké hovno"));
		AddItem (new Item ("CHEESE","Sýr","Ementálek z večerky. Pro myšku, nebo k svačině."));
		AddItem (new Item ("CAT","Kočka","Umí se zle tvářit a ráda jí myši. O dva skilly více, než průměrná městská kočka mívá."));	
		AddItem (new Item ("BURGER","Zkažený burger","Po dvou týdnech na čerstvém vzduchu je zapomenutá okurka to poslední, čím je tento burger zkažený."));	
		AddItem (new Item ("DOG","Rocky","Výstavní čivava paní Jedličkové. Jen ty trávicí potíže kdyby neměl..."));
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
