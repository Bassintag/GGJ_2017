using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    private SpriteRenderer _renderer;
    public Sprite _down;
    public Sprite _up;
    public bool _oncePush = false;
    public Door door;

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_renderer.sprite == _up)
        {
            _renderer.sprite = _down;
            door.push -= 1;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (_renderer.sprite == _up)
        {
            _renderer.sprite = _down;
            door.push -= 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_renderer.sprite == _down && !_oncePush)
        {
            _renderer.sprite = _up;
            door.push += 1;
        }
    }

    public void reset()
    {
        if (_renderer.sprite == _down)
        {
            _renderer.sprite = _up;
            door.push += 1;
        }
    }
}
