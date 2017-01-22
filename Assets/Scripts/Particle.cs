using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public Vector2 velocity = new Vector2(0, 0);
    public float min_rotation_speed = 180f;
    public float max_rotation_speed = 360f;
    public float life_span = 3;

    private float _angle;
    private float _rotation_speed;

	void Start ()
    {
        _angle = Random.Range(0, 360);
        _rotation_speed = Random.Range(min_rotation_speed, max_rotation_speed);
	}
	
	void Update ()
    {
        if (life_span <= 0)
            Destroy(gameObject);
        life_span -= Time.deltaTime;
        _angle += _rotation_speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, _angle);
        transform.position += (Vector3)velocity * Time.deltaTime;
	}
}
