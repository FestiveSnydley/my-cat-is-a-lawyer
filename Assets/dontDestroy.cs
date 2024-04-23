using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All code written below this line was hand written

//To prevent errors/hidePrompt from being deleted once scene is exited. For convenience, also storing info abt spawn location
public class dontDestroy : MonoBehaviour
{
    public string startingPoint = "hallway";
    //private static GameObject instance = null;
    private dontDestroy[] objArray;
    private static dontDestroy savedLevelData;

    void Awake()
    {
       if(savedLevelData==null)
       {
        savedLevelData = this;
        savedLevelData.tag = "KeepObj";
        DontDestroyOnLoad(this);
       }

       else {
        Destroy(this);
       }
        /*
        objArray = FindObjectsOfType<dontDestroy>();
        if(objArray.Length > 1)
        {
            Destroy(objArray[1]); //shouldnt be larger than 2 
        }
        else
        DontDestroyOnLoad(gameObject);
        */
    }

    public static dontDestroy getSavedExit(){
        return savedLevelData;
    }
}
