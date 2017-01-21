using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bumper : MonoBehaviour {

    private float nextActionTime = 0.0f;
    public float initialBPM = 140;
    public UnityEvent onBeat;
    public float BPM { get { return 60 / _period; } set { _period = 60 / value; } }
    private float _period;

    private WaveEmitterAlt _emitter;

    void Start () {
        BPM = initialBPM;
    }

    void Update () {
        BumpSound();
    }

    void BumpSound()
    {
        if (Time.time > (nextActionTime * 2))
        {
            nextActionTime += _period;
            foreach (SpriteRenderer renderer in FindObjectsOfType<SpriteRenderer>())
            {
                if (renderer.CompareTag("Player"))
                    renderer.color = ColorProvider.instance.GetColor(1);
                else
                    renderer.color = ColorProvider.instance.GetColor();
            }
            onBeat.Invoke();
            ColorProvider.instance.NextColor();
        }
    }
}
