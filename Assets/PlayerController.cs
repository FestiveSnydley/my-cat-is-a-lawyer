using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//handwritten code begins after this line (the above was created upon script generation in unity)
using TMPro;
using UnityEngine.SceneManagement; //needed to change scenes

//littered with : if(hidePrompt != null) to avoid errors abt object not existing

public class playerController : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed = 2f;
    public TextMeshProUGUI hideText;
    public GameObject hidePrompt;
    private Rigidbody2D rigB; //set private bc it's refering to player object (this)
    private Vector2 movementDirection;
    private bool movementEnabled;
    private bool hiding = false;

    //respawn stuff
    private bool paintingRoomEntry = true;
    public Transform paintingRspwnPt; // for if the player is entering this room from the museum painting room
    public Transform  hallwayRspwnPt; //for if the player is entering this room from the other hallway
    public string hallScene;
    public string displayScene;

    //consistency stuff (ensuring that entry point is saved when scene is exited.)
    public dontDestroy savedScript;
    private string respawnLocation = "";
    public GameObject savedExitDataObj = null;

    // Start is called before the first frame update
    void Start()
    {
        
        //respawning -- accessing saved data
        savedExitDataObj = GameObject.FindWithTag("KeepObj");
        savedScript = savedExitDataObj.GetComponent<dontDestroy>();
        respawnLocation = savedScript.startingPoint;


        //actually setting rspwn pt
        if(respawnLocation == "paintingRoom")
        {
            player.transform.position = paintingRspwnPt.position;
        }
        else{
            player.transform.position = hallwayRspwnPt.position;
        }
        
        rigB = GetComponent<Rigidbody2D>();
        movementEnabled = true;
        //hidePrompt.SetActive(false);
        hideText.text = "Press [Z] to hide!";
        Time.timeScale = 1f; //unfreeze everything

        //respawn debugging:
        print(respawnLocation);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(movementEnabled)
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //get input from controller/keyboard

        //for hiding
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to exit!" || hideText.text == "Press [Z] to hide!")) //player is near something they can hide in
        {
            //entering hiding
            if (Input.GetKeyDown(KeyCode.Z) && !hiding)
            {
                hideText.text = "Press [Z] to exit!";
                
                print("Hiding activated");
                hiding = true;
                movementEnabled=false;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; //halt any previous movement
                GetComponent<Renderer>().enabled = false; //makes player character invisible (NOT set inactive-- this would disable current script)
            }
            //exiting hiding
            else if (Input.GetKeyDown(KeyCode.Z) && hiding)
            {
                hideText.text = "Press [Z] to hide!";
                print("Hiding deactivated");
                hiding = false;
                GetComponent<Renderer>().enabled = true; //makes player visible
                movementEnabled=true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.None;
            }
        }

        //for exiting -- to museum
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go towards the display room!"))
        {
             //change scene, update 
             if (Input.GetKeyDown(KeyCode.Z))
             {
                hidePrompt.SetActive(false);
                player.SetActive(false);
                SceneManager.LoadScene(displayScene);
             }
        }

        //for exiting -- to hallway
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go to the hallway!"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
             {
                hidePrompt.SetActive(false);
                player.SetActive(false); //done to prevent bug (calling ontriggerexit after scene change)
                SceneManager.LoadScene(hallScene);
             }
        }

    }

    void FixedUpdate() {
        if(movementEnabled)
        rigB.velocity = movementDirection*movementSpeed; //actual movement of character

        
    }

    void OnTriggerEnter2D(Collider2D other) //Recall that colliders are hit boxes. Also, TAGS ARE CASE SENSITIVE!!!
    {
        if (other.gameObject.CompareTag("Enemy") && !hiding) //restart level
       {
            Debug.Log("Restarting");
            movementEnabled = false;
            Time.timeScale = 0f; //freeze everything
            
            player.SetActive(false);

            //if(hidePrompt != null) {
            hideText.text = "Restarting...";
            hidePrompt.SetActive(true);
           // }

            //restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //doesn't restart scene properly, changing obj positions instead
            //player.transform.position = new Vector2(-0.01f, -3.635f);
            //movementEnabled = true;
        }

        if (other.gameObject.CompareTag("Hide")) //allow player the option to hide
       {
            //if(hidePrompt != null) {
            hideText.text = "Press [Z] to hide!";
            hidePrompt.SetActive(true); }
            //Debug.Log("Hiding");
            //movementEnabled = false;
       // }

        if(other.gameObject.CompareTag("Exit_Museum"))
        {
           // if(hidePrompt != null) {
            savedScript.startingPoint = "paintingRoom";
            hideText.text = "Press [Z] to go towards the display room!";
            hidePrompt.SetActive(true);
           // }
        }

        if(other.gameObject.CompareTag("Exit_Hallway"))
        {
           // if(hidePrompt != null) {
            savedScript.startingPoint = "hallway"; //setting here so there's time for the script/var to update before scene change
            hideText.text = "Press [Z] to go to the hallway!";
            hidePrompt.SetActive(true);
          //  }
        }

    }

    //hide prompt on exit
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hide") /*|| other.gameObject.CompareTag("Exit_Hallway") || other.gameObject.CompareTag("Exit_Museum")*/)
        {
           // if(hidePrompt != null)
            hidePrompt.SetActive(false);
        }

        if(other.gameObject.CompareTag("Exit_Hallway"))
        hidePrompt.SetActive(false);

        if(other.gameObject.CompareTag("Exit_Museum"))
        hidePrompt.SetActive(false);
    }

}
