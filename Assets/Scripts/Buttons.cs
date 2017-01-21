using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    private SpriteRenderer _renderer;
    public Sprite _down;
    public Sprite _up;
    public bool _oncePush = false;
    public Door door;
    private bool _isBlock = false;

    void Start () {
        _renderer = GetComponent<SpriteRenderer>();	
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if (_renderer.sprite == _up)
        {
            _renderer.sprite = _down;
            door.push -= 1;
        }
        if (other.tag != "Player")
            _isBlock = true;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (_renderer.sprite == _up)
        {
            _renderer.sprite = _down;
            door.push -= 1;
        }
        if (!_isBlock && other.tag != "Player")
            _isBlock = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!_isBlock && _renderer.sprite == _down && !_oncePush)
        {
            _renderer.sprite = _up;
            door.push += 1;
        }
        if (other.tag != "Player")
            _isBlock = false;
    }

    public void reset()
    {
        if (_renderer.sprite == _down)
        {
            _isBlock = false;
            _renderer.sprite = _up;
            door.push += 1;
        }
    }
}
