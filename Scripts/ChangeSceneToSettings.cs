using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToSettings : MonoBehaviour
{
public static bool hintClick;

    // Start is called before the first frame update
    // Auto
    void Start()
    {
        
    }

    // Update is called once per frame
    // Auto
    void Update()
    {
        
    }

    // Made by Ian
    public void OnClick()
    {
        string currentScene=SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(1);
    }

    public void OnClickSettings()
    {
        SceneManager.LoadScene(0);
    }

    public void HintClick()
    {
       
    hintClick = hintClick ? false : true;
    Debug.Log(hintClick);
    }
}
