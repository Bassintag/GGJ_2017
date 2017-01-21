using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {
    private Rigidbody2D _object;
    // Use this for initialization
    void Start () {
		_object = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision c)
    {
        float force = 3;

        if (c.gameObject.tag == "Player")
        {
            Vector3 dir = c.contacts[0].point - transform.position;

            dir = -dir.normalized;
            _object.velocity = new Vector2(force * dir.x, force * dir.y);
        }
    }
}
