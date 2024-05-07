using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update
    // Made by Ian
    void Start()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
    }

    // Update is called once per frame
    // Auto
    void Update()
    {
        
    }

    // Made by Ian
    public void Res1366_768()
    {
        Screen.SetResolution(1366, 768, false);
    }

    public void Res1440_900()
    {
        Screen.SetResolution(1440, 900, false);
    }

    public void Res1920_720()
    {
        Screen.SetResolution(1920, 720, false);
    }

    public void Res1920_1080_windowed()
    {
        Screen.SetResolution(1920, 1080, false);
    }

        public void Res1920_1080_full()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
    }
}
