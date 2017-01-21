using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
    private Rigidbody2D _object;
    private bool _pulse;
    private Renderer _renderer;
    private float nextActionTime = 0.0f;
    public float _period;


    // Use this for initialization
    void Start () {
		_object = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.white;
        setBpm();
    }

    // Update is called once per frame
    void Update () {
        BumpSound();
	}

    public void setBpm(float bpm = 140){
        _period = (float) (60/bpm);
    }

    void BumpSound()
    {
        if (Time.time > (nextActionTime * 2))
        {
            if (_renderer.material.color == Color.black)
                _renderer.material.color = Color.white;
            else
                _renderer.material.color = Color.black;
            nextActionTime += _period;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        float force = 3;

        if (c.gameObject.tag == "Player")
        {
            Vector3 dir = c.contacts[0].point - transform.position;

            dir = -dir.normalized;
            _object.velocity = new Vector2(force * dir.x, force * dir.y);
        }
    }
}
