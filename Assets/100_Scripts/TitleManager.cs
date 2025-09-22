using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource = GetComponent<AudioSource>();
        tutorialButton.onClick.AddListener(() => SceneManager.LoadScene("TutorialScene"));
        tutorialButton.onClick.AddListener(() => audioSource.PlayOneShot(clickSound));
        hardButton.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        hardButton.onClick.AddListener(() => audioSource.PlayOneShot(clickSound));
    }
}
