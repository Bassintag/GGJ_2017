using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour {

    public Vector2[] path;
    public float speed = 1;
    public bool looping = true;
    public bool rotate = true;

    private bool _reversed;
    private int _target_id;

    public Vector2 target { get { return path[_target_id]; } }

	void Start ()
    {
        Reset();
	}

    public void Reset()
    {
        _target_id = 0;
        transform.position = target;
    }

    void Update ()
    {
        Vector2 rel = target - (Vector2)transform.position;
        float rel_speed = speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-transform.position.y + target.y, -transform.position.x + target.x) * Mathf.Rad2Deg);
        if (rel.magnitude < rel_speed)
        {
            transform.position = target;
            if (looping || !_reversed)
            {
                _target_id = (_target_id + 1) % path.Length;
                if (!looping && _target_id == 0)
                {
                    _target_id = path.Length - 2;
                    _reversed = true;
                }
            }
            else
            {
                _target_id = (_target_id - 1);
                if (_target_id < 0)
                {
                    _target_id = 1;
                    _reversed = false;
                }
            }
        }
        else
            transform.position += (Vector3)rel.normalized * rel_speed;
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        int count = path.Length;
        if (!looping)
            count -= 1;
        for (int i = 0; i < count; i++)
        {
            Vector2 from = path[i];
            Vector2 to = path[(i + 1) % path.Length];
            Gizmos.DrawLine(from, to);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target, 0.2f);
    }
}
