using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mx;

    public void Start()
    {
        float f;
        mx.GetFloat(gameObject.name, out f);
        gameObject.GetComponent<Slider>().value = f;
    }

    public void SetVolume(float sliderValue)
    {
        mx.SetFloat(gameObject.name, Mathf.Log10(sliderValue) * 20);
    }
}
