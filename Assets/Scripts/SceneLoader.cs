using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class SceneLoader : MonoBehaviour {
    public string sceneName;

    private bool loading;
    private Canvas canvas;

    private void Start()
    {
        loading = false;
        canvas = GetComponent<Canvas>();
    }

    public void LoadScene()
    {
        loading = true;
        foreach (WaveEmitterAlt emitter in FindObjectsOfType<WaveEmitterAlt>())
            emitter.gameObject.SetActive(false);
        StartCoroutine(_LoadScene());
    }

    IEnumerator _LoadScene()
    {
        yield return new WaitForSeconds(.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
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
