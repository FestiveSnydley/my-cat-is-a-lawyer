using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TheSetting : MonoBehaviour
{

    // Reference to the UI button
    public Button button;

    // Boolean variable to keep track of the button state
    private bool isRed = false;


    public void GoMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleColor()
    {
        // If the button is currently red, change it to white; otherwise, change it to red
        if (isRed)
        {
            button.image.color = Color.white;
            isRed = false;
        }
        else
        {
            button.image.color = Color.red;
            isRed = true;
        }
    }

}