using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 0.01f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 0.01f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public float speed = 1000f;
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	public GameObject gamePane;

	private float screenWidth;
	private float screenHeight;
	private Camera camera;

	void Awake ()
	{

	}

	void Start() {
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		camera = GetComponent<Camera> ();
	}


	void Update ()
	{
		MouseWatch();
	}
	
	
	void MouseWatch()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;


		if(Input.mousePosition.x > screenWidth - xMargin)
			targetX = Mathf.Lerp(transform.position.x, transform.position.x + speed * Time.deltaTime, xSmooth * Time.deltaTime);

		else if(Input.mousePosition.x < 0 + xMargin)
			targetX = Mathf.Lerp(transform.position.x, transform.position.x - speed * Time.deltaTime, xSmooth * Time.deltaTime);

		if(Input.mousePosition.y > screenHeight - yMargin)
			targetY = Mathf.Lerp(transform.position.y, transform.position.y + speed * Time.deltaTime, ySmooth * Time.deltaTime);

		else if(Input.mousePosition.y < 0 + xMargin)
			targetY = Mathf.Lerp(transform.position.y, transform.position.y - speed * Time.deltaTime, ySmooth * Time.deltaTime);


		// The target x and y coordinates should not be larger than the maximum or smaller than the game pane
		var upper = camera.ScreenToWorldPoint(new Vector3 (0, screenHeight - camera.orthographicSize * screenHeight,0));
		var lower = camera.ScreenToWorldPoint(new Vector3 (0, camera.orthographicSize * screenHeight,0));

		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, upper.y, lower.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);


	}

	void OnGUI() 
	{
		GUI.Box( new Rect( (Screen.width / 2) - 140, 5, 280, 25 ), "Mouse Position = " );
		GUI.Box( new Rect( (Screen.width / 2) - 70, Screen.height - 30, 140, 25 ), "Mouse X = " + Input.mousePosition.x );
		GUI.Box( new Rect( 5, (Screen.height / 2) - 12, 140, 25 ), "Mouse Y = " + Input.mousePosition.y );
	}
}
