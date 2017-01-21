using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProvider : MonoBehaviour {

    public static ColorProvider instance { get; private set; }

    public Color color_1;
    public Color color_2;
    public Color color_3;

    public Color[] colors { get { return new Color[3] { color_1, color_2, color_3 }; } }

    private int current_color = 0;

	void Awake ()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
	}

    public void NextColor()
    {
        current_color = (current_color + 1) % 3;
    }

    public Color GetColor(int offset=0)
    {
        return (colors[(current_color + offset) % 3]);
    }
}
