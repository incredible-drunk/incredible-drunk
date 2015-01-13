using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemPlacement : MonoBehaviour {
	public GameObject ToInstantiate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {


			var vector3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
			var inventory = GameObject.FindGameObjectWithTag("Inventory");
			Debug.Log(vector3);



			if(inventory != null){
				var colider = Physics2D.OverlapPoint(vector3,LayerMask.GetMask("Selection"));
				Debug.Log("Clicked on colider: " + colider);
				if(colider != null){
					inventory.GetComponent<Inventory>().GamePlayObjectClicked(colider.transform.parent.gameObject);
				}else{
					inventory.GetComponent<Inventory>().PlaceItemIntoWorld(vector3);
				}

			}

		    //GameObject instance = (GameObject)Instantiate(ToInstantiate,vector3,Quaternion.identity);
		}
	}

}
