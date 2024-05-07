using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//handwritten code begins after this line (the above was created upon script generation in unity)
using TMPro;
using UnityEngine.SceneManagement; //needed to change scenes

//written for hallway B.. probably more efficient ways to do this but 
public class playerController_B : MonoBehaviour
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
    private string entryPoint = "hallway"; //using this rather than a bool to determine room origin since there's 4 possibilities now
    public Transform  hallwayRspwnPt; //for if the player is entering this room from the other hallway
    public Transform  officeRspwnPt; //for if the player is entering this room from the office
    public Transform  securityRspwnPt; //for if the player is entering this room from the sec room
    public Transform  breakRspwnPt; //for if the player is entering this room from the break room
    //other scenes
    public string hallScene;
    public string officeScene;
    public string securityScene;
    public string breakScene;

    //consistency stuff (ensuring that entry point is saved when scene is exited.)
   public dontDestroyB savedScript;
    private string respawnLocation = "";
    public GameObject savedExitDataObj = null;

    // Start is called before the first frame update
    void Start()
    {
        //respawning
        savedExitDataObj = GameObject.FindWithTag("KeepObjB");
        savedScript = savedExitDataObj.GetComponent<dontDestroyB>();
        respawnLocation = savedScript.startingPoint;
        
        if(respawnLocation == "hallway")
        {
            player.transform.position = hallwayRspwnPt.position;
        }

        if(respawnLocation == "office")
        {
            player.transform.position = officeRspwnPt.position;
        }

        if(respawnLocation == "security")
        {
            player.transform.position = securityRspwnPt.position;
        }

        if(respawnLocation == "break")
        {
            player.transform.position = breakRspwnPt.position;
        }
        
        rigB = GetComponent<Rigidbody2D>();
        movementEnabled = true;
        //hidePrompt.SetActive(false);
        hideText.text = "Press [Z] to hide!";
        Time.timeScale = 1f; //unfreeze everything
    }

    // Update is called once per frame
    void Update()
    {
        if(movementEnabled)
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //get input from controller/keyboard

        //rotate -- set to player's rb2d in fixed update
        if(movementDirection != Vector2.zero) {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg; //get angle direction
            transform.rotation = Quaternion.AngleAxis(angle+270, Vector3.forward);
        }


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
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //keep rotation from glitching out (freeze Z)
            }
        }

        //for exiting -- to hallway
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go to the hallway!"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
             {
                entryPoint = "hallway";
                hidePrompt.SetActive(false);
                player.SetActive(false);
                SceneManager.LoadScene(hallScene);
             }
        }

        //for exiting -- to office
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go to the office!"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
             {
                entryPoint = "office";
                hidePrompt.SetActive(false);
                player.SetActive(false);
                SceneManager.LoadScene(officeScene);
             }
        }

        //for exiting -- to break
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go to the security room!"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
             {
                entryPoint = "security";
                hidePrompt.SetActive(false);
                player.SetActive(false);
                SceneManager.LoadScene(securityScene);
             }
        }

        //for exiting -- to security
        if(hidePrompt.activeSelf && (hideText.text == "Press [Z] to go to the break room!"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
             {
                entryPoint = "break";
                hidePrompt.SetActive(false);
                player.SetActive(false);
                SceneManager.LoadScene(breakScene);
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
            hideText.text = "Restarting...";
            hidePrompt.SetActive(true);

            player.SetActive(false);

            //restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //doesn't restart scene properly, changing obj positions instead
            //player.transform.position = new Vector2(-0.01f, -3.635f);
            //movementEnabled = true;
        }

        if (other.gameObject.CompareTag("Hide")) //allow player the option to hide
       {
            hideText.text = "Press [Z] to hide!";
            hidePrompt.SetActive(true);
            //Debug.Log("Hiding");
            //movementEnabled = false;
        }

        //exiting room prompts
        if(other.gameObject.CompareTag("Exit_Hallway"))
        {
            hideText.text = "Press [Z] to go to the hallway!";
            savedScript.startingPoint = "hallway";
            hidePrompt.SetActive(true);
        }

        if(other.gameObject.CompareTag("Exit_Office"))
        {
            hideText.text = "Press [Z] to go to the office!";
            savedScript.startingPoint = "office";
            hidePrompt.SetActive(true);
        }

        if(other.gameObject.CompareTag("Exit_Security"))
        {
            hideText.text = "Press [Z] to go to the security room!";
            savedScript.startingPoint = "security";
            hidePrompt.SetActive(true);
        }

        if(other.gameObject.CompareTag("Exit_Break"))
        {
            hideText.text = "Press [Z] to go to the break room!";
            savedScript.startingPoint = "break";
            hidePrompt.SetActive(true);
        }


    }

    //hide prompt on exit
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Hide") /*|| other.gameObject.CompareTag("Exit_Hallway") || other.gameObject.CompareTag("Exit_Office") || other.gameObject.CompareTag("Exit_Security") || other.gameObject.CompareTag("Exit_Break") */)
        {
             if(hidePrompt != null) //needed to prevent err
            hidePrompt.SetActive(false);
        }

        if(other.gameObject.CompareTag("Exit_Hallway"))
        hidePrompt.SetActive(false);
        if(other.gameObject.CompareTag("Exit_Office"))
        hidePrompt.SetActive(false);
        if(other.gameObject.CompareTag("Exit_Security"))
        hidePrompt.SetActive(false);
        if(other.gameObject.CompareTag("Exit_Break"))
        hidePrompt.SetActive(false);
    }

}
