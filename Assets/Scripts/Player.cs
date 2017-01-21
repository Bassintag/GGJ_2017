using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    enum Tristate
    {
        NONE = 0,
        RIGHT = 1,
        TOP = 1,
        LEFT = -1,
        BOT = -1
    };


    public float maxSpeed = 10f;
    private Rigidbody2D player;
    private Vector3 _origPos;

    void Start () {
        player = GetComponent<Rigidbody2D>();
        _origPos = player.position;
    }
	
	void Update () {
        move();
        _origPos = player.position;
    }

    void move()
    {
        float   moveHorizon = Input.GetAxis("Horizontal"),
                moveVertical = Input.GetAxis("Vertical");

        player.velocity = new Vector2(maxSpeed * moveHorizon, maxSpeed * moveVertical);
        Vector3 moveDirection = player.velocity.normalized;

        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg % 180;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
