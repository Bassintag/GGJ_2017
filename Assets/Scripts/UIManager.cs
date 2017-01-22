using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager instance { get; private set; }

    private float start;
    private int deaths;

	void Start () {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(this);
        start = Time.time;
        deaths = 0;
	}

    public void Reset ()
    {
        start = Time.time;
        deaths = 0;
    }

    public void AddDeath()
    {
        deaths++;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
