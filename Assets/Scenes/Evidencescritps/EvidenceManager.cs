using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//MADE BY MARKUS
public class EvidenceManager : MonoBehaviour
{
    public TextMeshProUGUI evidenceText;
    private int evidenceCount = 0;

    void Start()
    {
        LoadEvidenceCount(); // Load the evidence count when the game starts
    }

    void UpdateEvidenceText()
    {
        evidenceText.text = "Evidence: " + evidenceCount.ToString();
    }

    public void IncrementEvidenceCount()
    {
        evidenceCount++;
        UpdateEvidenceText();
        SaveEvidenceCount(); // Save the evidence count after incrementing
    }

    void SaveEvidenceCount()
    {
        PlayerPrefs.SetInt("EvidenceCount", evidenceCount);
        PlayerPrefs.Save(); // Save the PlayerPrefs data
    }

    void LoadEvidenceCount()
    {
        evidenceCount = PlayerPrefs.GetInt("EvidenceCount", 0);
        UpdateEvidenceText(); // Update the TextMeshPro text after loading
    }

    public void ResetEvidenceCount()
    {
        evidenceCount = 0; // Reset the evidence count
        UpdateEvidenceText(); // Update the TextMeshPro text
        SaveEvidenceCount(); // Save the reset evidence count
    }

}
