using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class SceneLoader : MonoBehaviour {
    public static SceneLoader instance { get; private set; }

    private bool loading;
    private Canvas canvas;

    private void Start()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        loading = false;
        canvas = GetComponent<Canvas>();
    }

    public void LoadScene(string name)
    {
        loading = true;
        foreach (WaveEmitterAlt emitter in FindObjectsOfType<WaveEmitterAlt>())
            emitter.gameObject.SetActive(false);
        StartCoroutine(_LoadScene(name));
    }

    IEnumerator _LoadScene(string name)
    {
        yield return new WaitForSeconds(.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void Update()
    {
        if (loading)
        {
            canvas.sortingOrder = 1;
            canvas.sortingLayerName = "Overlay";
            loading = false;
        }
    }
}
