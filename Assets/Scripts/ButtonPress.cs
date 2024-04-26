using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonPress : MonoBehaviour
{
    public GameObject painting;
    public Color originalColor;
    public Color newColor;
    private Image image;
    private int buttonToggle=0;
    // Start is called before the first frame update
    // Auto
    void Start()
    {

    }

    // Update is called once per frame
    // Made by Ian
    void Update()
    {

        //if(painting.hintClick==true)
        //{
        //image=GetComponent<Image>();
        //image.color=newColor;
        //}
    }
    // Made by Ian
    public void OnClick()
    {
        if(buttonToggle==0){
        buttonToggle=1;
        //Debug.Log("Clicked");
        image=GetComponent<Image>();
        image.color=newColor;
        }
        else{
        buttonToggle=0;
        //Debug.Log("Unclicked");
        image=GetComponent<Image>();
        image.color=originalColor;
        }
    }
}
