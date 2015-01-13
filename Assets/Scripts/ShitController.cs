using UnityEngine;
using System.Collections;

public class ShitController : MonoBehaviour
{

    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            takeShit();
            Debug.Log("Takin shit, my lord!");
        }
        
    }

    public void spawnShit()
    {
        Debug.Log("Spawn the shit right here!");
    }

    private void takeShit()
    {
        anim.SetTrigger("takePoo");
    }
}
 