using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    public Vector2[] path;
    public float speed = 1;
    public bool looping = true;

    private bool _reverse;
    private int _target_id;

    public Vector2 target { get { return path[_target_id]; } }

	void Start ()
    {
        _target_id = 0;
	}
	
	void Update ()
    {
        Vector2 rel = target - (Vector2)transform.position;
        float rel_speed = speed * Time.deltaTime;
        if (rel.magnitude < rel_speed)
        {
            transform.position = target;
            _target_id = (_target_id + 1) % path.Length;
        }
        else
            transform.position += (Vector3)rel.normalized * rel_speed;
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < path.Length; i++)
        {
            Vector2 from = path[i];
            Vector2 to = path[(i + 1) % path.Length];
            Gizmos.DrawLine(from, to);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target, 0.2f);
    }
}
