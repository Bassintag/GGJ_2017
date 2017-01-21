using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    private SpriteRenderer _renderer;
    public Sprite _down;
    public Sprite _up;
    public bool _oncePush = false;
    private bool _isPushed = false;
    public Door door;

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool getIsPushed()
    {
        return _isPushed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_oncePush && _renderer.sprite == _up)
        {
            _renderer.sprite = _down;
            _isPushed = true;
            door.push -= 1;
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (_renderer.sprite == _up && !_oncePush)
        {
            _isPushed = true;
            _renderer.sprite = _down;
            door.push -= 1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_renderer.sprite == _down && !_oncePush)
        {
            _renderer.sprite = _up;
            _isPushed = false;
            door.push += 1;
        }
    }
}
