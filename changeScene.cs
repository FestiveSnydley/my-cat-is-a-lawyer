using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //needed to change scenes

public class changeScene : MonoBehaviour
{
    //get access to a game object in script
    public GameObject changeSceneButton; //this is the sprite
    public string otherScene; //the other scene

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        Debug.Log("Box sprite clicked. Changing scene.");
        SceneManager.LoadScene(otherScene);
    }
}
