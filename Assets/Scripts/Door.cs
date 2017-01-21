using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Renderer _renderer;
    private Collider2D _collider;
    public int push = 0;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (push == 0)
        {
            _renderer.enabled = false;
            _collider.enabled = false;
        }
        else
        {
            _renderer.enabled = true;
            _collider.enabled = true;
        }
    }
}
