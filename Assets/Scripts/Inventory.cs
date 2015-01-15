using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IGameStateListener {

	public GameObject SlotPrefab;
	public List<GameObject> Slots = new List<GameObject>();
	public GameObject Tooltip;
	public GameObject      DraggedItemImage;
	private GameState GameState;
	public List<Item> Items = new List<Item> ();
	private List<PlacedItem> _placedItems  = new List<PlacedItem> ();
	private SlotScript draggingSlotScript = null;
	public Item draggedItem = null;
	public bool mouseInsideInventory = false;
	ItemDatabase databse;
	public AudioSource audioSource;

	float firsSlotX = -278.2f;
	// Use this for initialization
	void Start () {

	}

	void Awake(){
		databse = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
		GameState = GameObject.Find ("GameState").GetComponent<GameState> ();
		GameState.RegisterGameStateListener (this);
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (draggedItem != null) {
			Vector3 dragImagePos = (Input.mousePosition - GameObject.Find("Canvas").GetComponent<RectTransform>().localPosition);
			DraggedItemImage.GetComponent<RectTransform>().localPosition = dragImagePos;
			if(GameState.State != GameStates.Planning){
				CancelDrag();
			}
		}
		if (GameState.State == GameStates.Planning) {

			this.gameObject.GetComponent<Image>().enabled = true;
		} else {


		}
	
	}

	public void StartDrag(SlotScript slotScript){
		draggingSlotScript = slotScript;
		draggedItem = slotScript.item;
		PlayItemProtagonistSpeech (draggedItem);
		slotScript.item = null;
		ShowDraggedItemImage (draggedItem);

	}

	public void PlaceItemIntoWorld(Vector3 vector){
		if (draggedItem != null && mouseInsideInventory == false && GameState.IsWithingPlayableArea(vector)) {
			GameObject gameObject = (GameObject)Instantiate(draggedItem.InGameObjectPrefab);
			gameObject.transform.position = vector;
			var placedItem = new PlacedItem(draggedItem,gameObject);
			placedItem.placementPosition = vector;
			_placedItems.Add(placedItem);
			draggedItem = null;
			draggingSlotScript = null;
			this.DraggedItemImage.SetActive (false);
			MakeDrunkCommentOnPlacement(placedItem.Item);


		}
	}

    private void MakeDrunkCommentOnPlacement(Item item){

		var drunk = GameObject.FindGameObjectWithTag("Drunk");
		var drunkAudioSource = drunk.GetComponent<AudioSource>();
		float delta = 0;

		if(drunkAudioSource.isPlaying){
			return;
		}
		Debug.Log ("Making drunk comment on " + item.Name);
		if (audioSource.isPlaying) {
			delta = audioSource.clip.length - audioSource.time + 0.5f;
		}
		if (item.SoundBank != null) {
						drunk.GetComponent<DrunkController> ().CommentOnPlacement (item.GetNextDrunkClip(), delta);
		} else {
			Debug.Log ("No soundbank for drunk to comment on " + item.Name);
		}
	
	}

	public void GamePlayObjectClicked(GameObject gameObject){
		if (GameState.State == GameStates.Planning && draggedItem == null) {
			//Look through all placed items and find the right one
			PlacedItem clickedPlacedItem = null;
			foreach(PlacedItem placedItem in _placedItems){
				if(placedItem.InGameItem == gameObject){
					clickedPlacedItem = placedItem;
					break;
				}
			}
			if(clickedPlacedItem != null){
				draggedItem = clickedPlacedItem.Item;
				ShowDraggedItemImage(draggedItem);
				Destroy(clickedPlacedItem.InGameItem);
				_placedItems.Remove(clickedPlacedItem);
			}
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
	public void PlayItemProtagonistSpeech(Item item){
		var drunk = GameObject.FindGameObjectWithTag ("Drunk");
		if (drunk != null && drunk.GetComponent<AudioSource> ().isPlaying) {
			return;
		}

		if (item.SoundBank != null && audioSource.isPlaying == false) {
			Debug.Log("Playing osund for " + item.Name);
			var clip = item.GetNextProtagonistClip();
			audioSource.clip = clip;
			audioSource.Play();
		} else if(item.SoundBank == null) {
			Debug.Log("No sound bank for " + item.Name);		
		}
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

	#region IGameStateListener implementation

	public void OnGameStateChange (GameStates oldStates, GameStates newState)
	{
		if (newState == GameStates.Planning) {
			if(oldStates == GameStates.Planning || oldStates == GameStates.Intro){
				DestroyPlacedItems ();
				ResetInventory ();
			}else{
				foreach (GameObject slot in Slots) {
					slot.SetActive(true);
				}
				ResetPlacedItems();
			}
		} else if (newState == GameStates.Simulation || newState == GameStates.Intro) {
			this.gameObject.GetComponent<Image>().enabled = false;
			foreach (GameObject slot in Slots) {
				slot.SetActive(false);
			}
						
		}
	}

	private void ResetPlacedItems(){
		foreach(PlacedItem placedItem in _placedItems){
			if(placedItem.InGameItem != null){
				Destroy(placedItem.InGameItem);
			}
			placedItem.InGameItem = (GameObject)Instantiate(placedItem.Item.InGameObjectPrefab);
			placedItem.InGameItem.transform.position = placedItem.placementPosition;
		}
	}

	private void ResetInventory(){
		foreach (GameObject slot in Slots) {
			DestroyObject(slot);
		}
		Slots.Clear ();



		for (int i = 0; i < 8; i++) {
			GameObject slot = (GameObject)Instantiate(SlotPrefab);
			Slots.Add(slot);
			slot.transform.SetParent(this.gameObject.transform);
			slot.name = "Slot"+i;
			slot.GetComponent<RectTransform>().localPosition = new Vector3(firsSlotX + i*(65+10),0,0) ;
		}
		addItem("MOUSE");
		addItem ("CUTTERS");
		//addItem ("POO");
		addItem ("DOG");
		addItem ("CHEESE");
		addItem ("CAT");
		addItem ("BURGER");
	}

	private void DestroyPlacedItems(){
		Debug.Log("Inventory is destorying placed items");
		foreach(PlacedItem placedItem in _placedItems){
			Destroy(placedItem.InGameItem);
		}
		_placedItems.Clear();
	}

	#endregion

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
