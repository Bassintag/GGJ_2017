using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundSound : MonoBehaviour {

    public AudioClip _intro, _music;
    private AudioSource _source;

	// Use this for initialization
	void Start () {
        _source = GetComponent<AudioSource>();
        _source.clip = _intro;
        _source.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (_source.isPlaying == false)
        {
            _source.clip = _music;
            _source.loop = true;
            _source.Play();
        }
    }
}
