using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject SlotPrefab;
	public List<GameObject> Slots = new List<GameObject>();
	public GameObject Tooltip;
	public List<Item> Items = new List<Item> ();
	ItemDatabase databse;
	float firsSlotX = -278.2f;
	// Use this for initialization
	void Start () {
		databse = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
		for (int i = 0; i < 8; i++) {
			GameObject slot = (GameObject)Instantiate(SlotPrefab);
			Slots.Add(slot);
			slot.transform.SetParent(this.gameObject.transform);
			slot.name = "Slot"+i;
			slot.GetComponent<RectTransform>().localPosition = new Vector3(firsSlotX + i*(65+10),0,0) ;
		}
		addItem("MOUSE");
		addItem ("CUTTERS");
		addItem ("POO");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void addItem(string itemId){
		var item = databse.Items[itemId];
		GameObject slot = FindFirstEmptySlot ();
		slot.GetComponent<SlotScript> ().item = item;

	}

	GameObject FindFirstEmptySlot(){
		foreach(GameObject slot in this.Slots){
			if(slot.GetComponent<SlotScript>().item == null){
				return slot;
			}
		}
		return null;
	}

	public void ShowTooltip(Vector3 tooltipPos,Item item){
		Tooltip.SetActive (true);
		Text itemNameText = Tooltip.transform.Find ("ItemName").GetComponent<Text>();
		Text itemDescriptionText = Tooltip.transform.Find ("ItemDescription").GetComponent<Text> ();

		itemNameText.text = item.Name;
		itemDescriptionText.text = item.Description;
	}
	public void HideTooltip(){
		Tooltip.SetActive (false);
	}
}
