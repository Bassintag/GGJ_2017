using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    public float maxSpeed = 10f;
    private Rigidbody2D player;

    void Start () {
        player = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        move();
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

    void OnCollisionEnter(Collision c)
    {
        Debug.Log(c.gameObject.tag);

        if (c.gameObject.tag == "Finish")
        {
            Debug.Log(SceneManager.GetActiveScene());

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
    }
}
