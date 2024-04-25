using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//MADE BY MARKUS
public class SceneResetter : MonoBehaviour
{
    public GameObject Evidence;
    public GameObject Evidence2;
    public GameObject Evidence3;
    public EvidenceManager evidenceManager;


    public void ResetScene()
    {
        // Reset the evidence flags to false
        evidenceManager.ResetEvidenceCount();

        // Reset the evidence GameObjects to active
        Evidence.SetActive(true);
        Evidence2.SetActive(true);
        Evidence3.SetActive(true);

        // Save the reset evidence state
        SaveEvidenceState();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SaveEvidenceState()
    {
        PlayerPrefs.SetInt(Evidence.name + "_Retrieved", 0);
        PlayerPrefs.SetInt(Evidence2.name + "_Retrieved", 0);
        PlayerPrefs.SetInt(Evidence3.name + "_Retrieved", 0);
        PlayerPrefs.Save();
    }

}
