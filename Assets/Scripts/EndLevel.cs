using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class EndLevel : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int lvl = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 6));
            SceneManager.LoadScene("Level-" + (lvl + 1));
        }
    }   
}
