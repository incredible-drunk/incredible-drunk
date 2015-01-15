using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour, IGameStateListener
{
    public float xMargin = 2f;		// Distance in the x axis the player can move before the camera follows.
    public float yMargin = 2f;		// Distance in the y axis the player can move before the camera follows.
    public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
    public float speed = 20f;

    public float minZoom = 2f;
    private float maxZoom = 8f;

    public GameObject gamePane;

    private float screenWidth;
    private float screenHeight;
    private Camera camera;
    private GameState gameState;

    private float mapX;
    private float mapY;

	public float dampTime = 2.55f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

    void Awake()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        gameState.RegisterGameStateListener(this);
    }

    void Start()
    {
        camera = GetComponent<Camera>();
        var renderer = gamePane.GetComponent<Renderer>();
        mapX = renderer.bounds.size.x;
        mapY = renderer.bounds.size.y;

    }


    void Update()
    {
		if (target)
		{
			//Vector3 point = camera.WorldToViewportPoint(target.position);
			//Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = target.position; //+ delta;
			transform.position = Vector3.SmoothDamp(transform.position, LimitTargetFollow(destination), ref velocity, dampTime);




		}else if (gameState.State == GameStates.Planning || gameState.State == GameStates.Simulation)
        {
            MouseWatch();
			transform.position = LimitTargetFollow (transform.position);
        }

    }

	private Vector3 LimitTargetFollow(Vector3 targetTransform){
		var targetX = targetTransform.x;
		var targetY = targetTransform.y;

		var vertExtent = Camera.main.camera.orthographicSize;
		var horzExtent = vertExtent * Screen.width / Screen.height;
		
		// Calculations assume map is position at the origin
		var minX = horzExtent - mapX / 2.0f + gamePane.transform.position.x;
		var maxX = mapX / 2.0f - horzExtent + gamePane.transform.position.x;
		var minY = vertExtent - mapY / 2.0f + gamePane.transform.position.y;
		var maxY = mapY / 2.0f - vertExtent + gamePane.transform.position.y;
		
		targetX = Mathf.Clamp(targetX, minX, maxX);
		targetY = Mathf.Clamp(targetY, minY, maxY);	
		return new Vector3(targetX, targetY, transform.position.z);
	}


    void MouseWatch()
    {

        maxZoom = Mathf.Min(mapY, mapX / Screen.width * Screen.height) / 2;

        //zoom in zoom out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        { // forward
            camera.orthographicSize -= 1f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        { // back
            Camera.main.orthographicSize += 1f;
        }

        //limit zoom on resize
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        // By default the target x and y coordinates of the camera are it's current x and y coordinates.
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (Input.mousePosition.x > Screen.width - xMargin)
            targetX = Mathf.Lerp(transform.position.x, transform.position.x + speed * Time.deltaTime, xSmooth * Time.deltaTime);

        else if (Input.mousePosition.x < 0 + xMargin)
            targetX = Mathf.Lerp(transform.position.x, transform.position.x - speed * Time.deltaTime, xSmooth * Time.deltaTime);

        if (Input.mousePosition.y > Screen.height - yMargin)
            targetY = Mathf.Lerp(transform.position.y, transform.position.y + speed * Time.deltaTime, ySmooth * Time.deltaTime);

        else if (Input.mousePosition.y < 0 + xMargin)
            targetY = Mathf.Lerp(transform.position.y, transform.position.y - speed * Time.deltaTime, ySmooth * Time.deltaTime);



        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);


    }



    #region IGameStateListener implementation

    public void OnGameStateChange(GameStates oldStates, GameStates newState)
    {
        if (newState == GameStates.Intro)
        {
			target = null;
            GetComponent<Animator>().SetTrigger("IntroStartT");
        }
        else if (newState == GameStates.Planning)
        {
			target = null;
            GetComponent<Animator>().enabled = false;
        }else if(newState == GameStates.GameOverLose){
			//target = GameObject.FindGameObjectWithTag("Drunk").transform;
		}
    }

    #endregion
}
