using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private static SettingsController instance;
    // Start is called before the first frame update
    void Start()
    {
      if (instance == null)
      {
            instance = this;
            DontDestroyOnLoad(gameObject);
      }
      else
      {
            Destroy(gameObject);
      }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
