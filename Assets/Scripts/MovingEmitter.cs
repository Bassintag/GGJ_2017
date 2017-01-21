using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEmitter : MonoBehaviour {

    public WaveEmitterAlt prefab;
    public Vector2[] waypoints;
    public bool loop = true;
    public float speed = 1f;

    private Vector2 _target;
    private int _target_id;

    void Start()
    {
        _target_id = 0;
        _target = waypoints[_target_id];
    }

	void Update ()
    {
        Vector3 rel = (_target - (Vector2)transform.position).normalized * speed * Time.deltaTime;
        transform.position += rel;
	}
}
