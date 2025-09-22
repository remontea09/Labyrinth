using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleButton : MonoBehaviour
{

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        exitButton.onClick.AddListener(() => GameExit());
    }

    private void GameExit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


}
