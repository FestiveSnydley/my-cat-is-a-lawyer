using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer soundMixer;
    // Start is called before the first frame update
    // Made by Ian
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    // Update is called once per frame
    // Auto
    void Update()
    {
        
    }

    // Made by Ian
    public void SetVolume(float _value)
    {
        if (_value <1)
        {
            _value=.001f;
        }
        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        soundMixer.SetFloat("MasterVolume", Mathf.Log10(_value/100)*20f);
    }

    public void SetSliderVolume(){
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float _value)
    {
        soundSlider.value=_value;
    }
}