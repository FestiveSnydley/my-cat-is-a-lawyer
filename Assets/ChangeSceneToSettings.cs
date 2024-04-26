using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToSettings : MonoBehaviour
{
    public static bool hintClick;

    public static string previousScene;
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
        previousScene=SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
    }

    public void OnClickSettings()
    {

        SceneManager.UnloadScene("OptionsMenu");
    }

    public void HintClick()
    {
       
    hintClick = hintClick ? false : true;
    Debug.Log(hintClick);
    }
}
