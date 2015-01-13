using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemPlacement : MonoBehaviour, IPointerDownHandler {
	public GameObject ToInstantiate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			var vector3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			Debug.Log(vector3);
			var inventory = GameObject.FindGameObjectWithTag("Inventory");
			if(inventory != null){
				inventory.GetComponent<Inventory>().PlaceItemIntoWorld(vector3);
			}

		    //GameObject instance = (GameObject)Instantiate(ToInstantiate,vector3,Quaternion.identity);
		}
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{

	}

	#endregion
}
