using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public Button playButton;
    public Button exitButton;
    public SceneLoader loader;

    public void PlayButtonClick()
    {
        playButton.interactable = false;
        UIManager.instance.Reset();
        loader.LoadScene();
    }

    public void ExitButtonClick()
    {
        exitButton.interactable = false;
        Application.Quit();
    }
}
