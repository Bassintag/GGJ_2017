using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaveEmitterAlt))]
public class Bumper : MonoBehaviour {

    private float nextActionTime = 0.0f;
    public float initialBPM = 140;
    public float BPM { get { return 60 / _period; } set { _period = 60 / value; } }
    private float _period;

    private WaveEmitterAlt _emitter;
    // Use this for initialization
    void Start () {
        _emitter = GetComponent<WaveEmitterAlt>();
        BPM = initialBPM;
    }

    // Update is called once per frame
    void Update () {
        BumpSound();
    }

    void BumpSound()
    {
        if (Time.time > (nextActionTime * 2))
        {
            nextActionTime += _period;
            _emitter.Emit();
        }
    }
}
