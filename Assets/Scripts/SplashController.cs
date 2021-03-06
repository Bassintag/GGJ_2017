﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {
    public Image splashImage;
    public float fadeInStart;
    public float fadeInEnd;
    public float fadeOutStart;
    public float fadeOutEnd;
    public float sceneSwitch;
    private float time;

    void Start () {
        time = 0.0f;
	}
	
	void Update () {
        time += Time.deltaTime;
        float fade = 1.0f;
        if (fadeInStart <= time && time <= fadeInEnd)
        {
            fade = 1 - (time - fadeInStart) / (fadeInEnd - fadeInStart);
        }
        else if (fadeInEnd <= time && time <= fadeOutStart)
        {
            fade = 0.0f;
        }
        else if (fadeOutStart <= time && time <= fadeOutEnd)
        {
            fade = (time - fadeOutStart) / (fadeOutEnd - fadeOutStart);
        }
        else if (time > sceneSwitch)
        {
            SceneLoader.instance.LoadScene("Menu");
        }
        splashImage.material.SetFloat("_Fade", fade);
	}
}
