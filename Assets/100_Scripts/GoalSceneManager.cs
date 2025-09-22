using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GoalSceneManager : MonoBehaviour
{

    [SerializeField] private Button exitButton;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        exitButton.onClick.AddListener(GameExitButton);
        exitButton.onClick.AddListener(() => audioSource.PlayOneShot(clickSound));
    }

    private void GameExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
