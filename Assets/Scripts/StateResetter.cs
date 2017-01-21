using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateResetter : MonoBehaviour {

    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _scale;

    void Start ()
    {
        _position = transform.position;
        _rotation = transform.rotation;
        _scale = transform.localScale;
	}
	
    public void Reset()
    {
        transform.position = _position;
        transform.rotation = _rotation;
        transform.localScale = _scale;
    }
}
