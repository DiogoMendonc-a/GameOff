using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject exitButton;

    private void Start() {
#if UNITY_WEBGL && !UNITY_EDITOR
    Destroy(exitButton);
#endif
    }

    public void NewGame() {
        GameManager.instance.StartNewGame();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
