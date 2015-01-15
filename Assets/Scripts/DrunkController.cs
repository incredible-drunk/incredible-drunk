using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DrunkState{
	Normal,
	InDeepShit,
	Rozmrd
}

public class DrunkController : MonoBehaviour {

	public float speed = 1f;
	public float minSecondsBetweenIdle = 4;
	public float maxSecondsBetweenIdle = 10;

	private float nextIdle = 0;
	private List<string> anims = new List<string> ();

	private GameState gameState;
	private DrunkState State = DrunkState.Normal;
	public float ShitClearingTime = 5;
	private float shitCleanedTime; 

	public AudioClip RozmrdSound = null;
	public AudioClip ShitAnnoyed = null;
	private AudioClip clipToPlay = null;

    private bool dodgedPiano = false;
    private bool dodgin = false;


    private Animator anim;

	void Awake() {

		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}

	// Use this for initialization
	void Start () {
		ComputeNextIdle ();
		Random.seed = System.DateTime.Today.Millisecond;
		anims.Add ("Tdrink");
		anims.Add ("Thead");
		anims.Add ("Tbend");
		anims.Add ("Tscratch");

        anim = GetComponent<Animator>();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
            Drink();

        if (Input.GetKeyDown(KeyCode.W))
            Head();

        if (Input.GetKeyDown(KeyCode.E))
            Point();

        if (Input.GetKeyDown(KeyCode.R))
            Bend();

        if (Input.GetKeyDown(KeyCode.T))
            Hand();
       
    }

	private void ComputeNextIdle(){
		nextIdle = Time.time + Random.Range (minSecondsBetweenIdle, maxSecondsBetweenIdle);
	}

	private void PlayIdle(){
		if (Time.time >= nextIdle) {
			if (gameState.State == GameStates.Planning || gameState.State == GameStates.Intro) {
				int random = Random.Range(0,anims.Count);
				
			    string pickedAnimT = anims.ToArray()[random];
				Debug.Log("Playing idle " + pickedAnimT);
				anim.SetTrigger(pickedAnimT);
			}		

			ComputeNextIdle ();
		}
	}
	
	void FixedUpdate () {
		PlayIdle ();
		if (gameState.State == GameStates.Simulation) {
			if(State == DrunkState.Normal && !dodgin){
				rigidbody2D.velocity = new Vector2(transform.localScale.x * speed, rigidbody2D.velocity.y);
	            Walk(); 
			}else if(State == DrunkState.InDeepShit){
				if(shitCleanedTime < Time.time){
					SetpOutOfShit();
				}
			}
		}

		if (transform.position.x > gameState.PlayableAreaMaxX) {
			gameState.State = GameStates.GameOverLose;
            Stop();
		}
	}

    private void Drink() {
        anim.SetTrigger("Tdrink");
    }

    private void Head() {
        anim.SetTrigger("Thead");
    }

    private void Point() {
        anim.SetTrigger("Tpoint");
    }

    private void Bend() {
        anim.SetTrigger("Tbend");
    }

    private void Hand()
    {
        anim.SetTrigger("Thand");
    }

    private void Walk()
    {
        anim.SetTrigger("Twalk");
    }

    private void Stop() {
        anim.SetTrigger("Tstop");
    }

    private void Dodge() {
        anim.SetTrigger("Tdodge");
    }

	private void StepInShit(){
		State = DrunkState.InDeepShit;
		anim.ResetTrigger("Twalk");
		anim.SetTrigger("Tshit");
		shitCleanedTime = Time.time + ShitClearingTime;

		var source = GetComponent<AudioSource> ();
		source.PlayOneShot (ShitAnnoyed);
	}

	private void SetpOutOfShit(){

		anim.SetTrigger("Twalk");
		anim.ResetTrigger("Tshit");
		Invoke ("SteppedOutOfShit", 1f);

	}
	public void SteppedOutOfShit(){
		if (State == DrunkState.InDeepShit) {
			State = DrunkState.Normal;
		}
	}

	public void DoRozmrd(){
		anim.ResetTrigger("Tshit");
		anim.SetTrigger("Trozmrd");
		State = DrunkState.Rozmrd;
		GetComponent<AudioSource> ().PlayOneShot (RozmrdSound);
	}

	public void CommentOnPlacement(AudioClip clip,float delay){
		if (clip != null) {
			if(clipToPlay != null){
				clipToPlay = null;
				CancelInvoke("CommentAfterDelay");
			}
			clipToPlay = clip;
			Debug.Log ("Will comment with " + clip.name + " after " + delay);
			Invoke("CommentAfterDelay",delay);
			//yield return new WaitForSeconds (delay);

		}
	}
	public void CommentAfterDelay(){
		if (clipToPlay == null) {
			return;		
		}
		var source = GetComponent<AudioSource> ();
		if (source.isPlaying == false) {
			Debug.Log ("Commenting on  " + clipToPlay.name );
			source.clip = clipToPlay;
			source.loop = false;
			source.Play ();
			Point ();
		}	
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject != null && other.gameObject.tag == "DogShit" && State == DrunkState.Normal){
			Debug.Log("Stepped in shit");
			StepInShit();
		}else if(other.gameObject != null && other.gameObject.tag == "Klavir"){
			switch(State) {
                case DrunkState.InDeepShit:
				    DoRozmrd();
                    break;
                case DrunkState.Normal:
                    DodgePiano();
                    break;
			}
		}
	}

    void DodgePiano () {
        if (!dodgedPiano) { 
            Dodge();
            rigidbody2D.velocity = new Vector2(transform.localScale.x * -2f, transform.localScale.y * 7f);
            dodgedPiano = dodgin = true;
        }
    }

    public void dodged() {
        dodgin = false;
    }



}
