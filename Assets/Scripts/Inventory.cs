using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject SlotPrefab;
	public List<GameObject> Slots = new List<GameObject>();
	public GameObject Tooltip;
	public GameObject      DraggedItemImage;
	private GameState GameState;
	public List<Item> Items = new List<Item> ();
	private SlotScript draggingSlotScript = null;
	public Item draggedItem = null;
	public bool mouseInsideInventory = false;
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
		addItem ("CHEESE");
	}

	void Awake(){
		GameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (draggedItem != null) {
			Vector3 dragImagePos = (Input.mousePosition - GameObject.Find("Canvas").GetComponent<RectTransform>().localPosition);
			DraggedItemImage.GetComponent<RectTransform>().localPosition = dragImagePos;
		}
	
	}

	public void StartDrag(SlotScript slotScript){
		draggingSlotScript = slotScript;
		draggedItem = slotScript.item;
		slotScript.item = null;
		ShowDraggedItemImage (draggedItem);
	}

	public void PlaceItemIntoWorld(Vector3 vector){
		if (draggedItem != null && mouseInsideInventory == false && GameState.IsWithingPlayableArea(vector)) {
			GameObject gameObject = (GameObject)Instantiate(draggedItem.InGameObjectPrefab);
			gameObject.transform.position = vector;
			draggedItem = null;
			draggingSlotScript = null;
			this.DraggedItemImage.SetActive (false);

		}
	}

	public void DropDraggedItemToSLot(SlotScript slotScript){
		slotScript.item = draggedItem;
		draggedItem = null;
		draggingSlotScript = null;
		this.DraggedItemImage.SetActive (false);
	}

	public void CancelDrag(){
		draggingSlotScript.item = draggedItem;
		draggedItem = null;
		draggingSlotScript = null;
		this.DraggedItemImage.SetActive (false);
	}

	private void ShowDraggedItemImage(Item item){
		this.DraggedItemImage.SetActive (true);
		this.DraggedItemImage.GetComponent<Image>().sprite = item.Icon;
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

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		mouseInsideInventory = true;
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		mouseInsideInventory = false;
	}

	#endregion
}
