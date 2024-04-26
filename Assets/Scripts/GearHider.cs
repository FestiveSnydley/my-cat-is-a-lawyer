using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GearHider : MonoBehaviour
{
    public static GameObject gear;
    // Start is called before the first frame update
    void Start()
    {
        // Find the object marked as "Don't Destroy On Load"
        gear = GameObject.Find("SettingsGear"); // Replace "YourTagHere" with the tag of the object

        if (gear != null)
        {
            return;
        }
        else
        {
            Debug.LogWarning("Object marked as 'SettingsGear' not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HideGear()
    {
        if (gear != null)
        {
            gear.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object marked as 'SettingsGear' not found.");
        }
        
    }
    public void ShowGear()
    {
        if (gear != null)
        {
            gear.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object marked as 'SettingsGear' not found.");
        }

    }
}
