using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All code written below this line was hand written

//To prevent errors/hidePrompt from being deleted once scene is exited. For convenience, also storing info abt spawn location
public class dontDestroyB : MonoBehaviour
{
    public string startingPoint = "hallway";
    //private static GameObject instance = null;
    private dontDestroyB[] objArray;
    private static dontDestroyB savedLevelData;

    void Awake()
    {
       if(savedLevelData==null)
       {
        savedLevelData = this;
        savedLevelData.tag = "KeepObjB";
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

    public static dontDestroyB getSavedExitB(){
        return savedLevelData;
    }
}
