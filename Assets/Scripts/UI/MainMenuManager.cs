using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject exitButton;
    public TMPro.TMP_InputField seed_input;

    private void Start() {
#if UNITY_WEBGL && !UNITY_EDITOR
    Destroy(exitButton);
#endif
    }

    public void NewGame() {
        int seed;
        bool parsed = int.TryParse(seed_input.text, out seed);
        if(!parsed || seed == 0) {
            seed = Random.Range(1, int.MaxValue);
        }
        GameManager.instance.StartNewGame(seed);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
