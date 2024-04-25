using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MADE BY MARKUS
public class EvidenceClicker : MonoBehaviour
{
    public GameObject Evidence;
    public GameObject Evidence2;
    public GameObject Evidence3;
    public EvidenceManager evidenceManager;
    private bool evidence1Retrieved = false;
    private bool evidence2Retrieved = false;
    private bool evidence3Retrieved = false;


    void Start()
    {
        LoadEvidenceState(); // Load the evidence state when the scene starts
    }

   

    public void RetrieveEvidence1()
    {
        if (!evidence1Retrieved)
        {
            Evidence.SetActive(false);
            evidence1Retrieved = true;
            SaveEvidenceState();
            if (evidenceManager != null)
            {
                evidenceManager.IncrementEvidenceCount();
            }
        }
    }

    public void RetrieveEvidence2()
    {
        if (!evidence2Retrieved)
        {
            Evidence2.SetActive(false);
            evidence2Retrieved = true;
            SaveEvidenceState();
            if (evidenceManager != null)
            {
                evidenceManager.IncrementEvidenceCount();
            }
        }
    }

    public void RetrieveEvidence3()
    {
        if (!evidence3Retrieved)
        {
            Evidence3.SetActive(false);
            evidence3Retrieved = true;
            SaveEvidenceState();
            if (evidenceManager != null)
            {
                evidenceManager.IncrementEvidenceCount();
            }
        }
    }

    void SaveEvidenceState()
    {
        PlayerPrefs.SetInt(Evidence.name + "_Retrieved", evidence1Retrieved ? 1 : 0);
        PlayerPrefs.SetInt(Evidence2.name + "_Retrieved", evidence2Retrieved ? 1 : 0);
        PlayerPrefs.SetInt(Evidence3.name + "_Retrieved", evidence3Retrieved ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadEvidenceState()
    {
        evidence1Retrieved = PlayerPrefs.GetInt(Evidence.name + "_Retrieved", 0) == 1;
        evidence2Retrieved = PlayerPrefs.GetInt(Evidence2.name + "_Retrieved", 0) == 1;
        evidence3Retrieved = PlayerPrefs.GetInt(Evidence3.name + "_Retrieved", 0) == 1;

        if (evidence1Retrieved)
        {
            Evidence.SetActive(false);
        }
        if (evidence2Retrieved)
        {
            Evidence2.SetActive(false);
        }
        if (evidence3Retrieved)
        {
            Evidence3.SetActive(false);
        }
    }
}