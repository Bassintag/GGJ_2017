using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSound : MonoBehaviour {

    private AudioSource _sound;
	// Use this for initialization
	void Start () {
        _sound = GetComponent<AudioSource>();
	}

    public void Play()
    {
        _sound.Play();
    }
}
