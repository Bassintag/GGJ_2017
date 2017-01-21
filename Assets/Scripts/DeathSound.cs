using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour {

    private AudioSource _sound;
	// Use this for initialization
	void Start () {
        _sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        _sound.Play();
    }
}
