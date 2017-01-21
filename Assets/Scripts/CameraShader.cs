using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraShader : MonoBehaviour
{
    public Shader shader;
    [Range(0.0f, 1.0f)]
    public float aberration;
    [Range(0.0f, 1.0f)]
    public float fadeout;
    private Material mat;

    void Awake()
    {
        mat = new Material(shader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Aberration", aberration);
        mat.SetFloat("_Fade", fadeout);
        Graphics.Blit(source, destination, mat);
    }
}