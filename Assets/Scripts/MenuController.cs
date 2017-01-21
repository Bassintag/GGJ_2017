using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public Button playButton;
    public Button exitButton;

    public void PlayButtonClick()
    {
        playButton.interactable = false;
        SceneManager.LoadScene("Level-1");
    }

    public void ExitButtonClick()
    {
        exitButton.interactable = false;
        Application.Quit();
    }
}
