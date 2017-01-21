using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer)]
public class Bumper : MonoBehaviour {

    private float nextActionTime = 0.0f;
    public float _period;
    private Renderer _renderer;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        setBpm();
    }

    // Update is called once per frame
    void Update () {
        BumpSound();
    }

    public void setBpm(float bpm = 140)
    {
        _period = (float)(60 / bpm);
    }

    void BumpSound()
    {
        if (Time.time > (nextActionTime * 2))
        {
            nextActionTime += _period;
            if (_renderer.material.color == Color.black)
                _renderer.material.color = Color.white;
            else
                _renderer.material.color = Color.black;
        }
    }
}
