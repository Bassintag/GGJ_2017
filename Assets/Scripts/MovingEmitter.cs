using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEmitter : MonoBehaviour {

    public WaveEmitterAlt prefab;
    public Vector2[] waypoints;
    public bool loop = true;
    public float speed = 1f;
    [HideInInspector]
    public bool paused = false;

    private Vector2 _target { get { return waypoints[_target_id]; } }
    private int _target_id;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _target_id = 0;
        transform.position = _target;
    }

    public void Emit()
    {
        if (paused)
            return;
        WaveEmitterAlt instance = Instantiate(prefab);
        instance.transform.position = transform.position;
        instance.moving_emitter = this;
        instance.auto_emit = true;
        instance.destroy_after = 1;
    }

    void Update ()
    {
        float rel_speed = speed * Time.deltaTime;
        Vector3 rel = (_target - (Vector2)transform.position);
        if (rel.magnitude <= rel_speed)
        {
            transform.position = _target;
            _target_id = (_target_id + 1) % waypoints.Length;
        }
        else
            transform.position += rel.normalized * rel_speed;
	}

    void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector2 from = waypoints[i];
            Vector2 to = waypoints[i == waypoints.Length - 1 ? 0 : i + 1];
            Gizmos.color = Color.green;
            Gizmos.DrawLine(from, to);
        }
    }
}
