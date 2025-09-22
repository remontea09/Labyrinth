using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    private PlayerController playerController;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Goal goal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    private async void Initialize()
    {
        await UniTask.WaitForSeconds(0.2f);
        Tutorial();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.ChangeCanMove();
        goal.OnGoal += TutorialGoal;
    }

    private async void Tutorial()
    {
        text.text = "����ɂ��́I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "���̃Q�[���̃`���[�g���A�������߂����܂��I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "�������͂��񂽂�I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "WASD�ł��ǂ��I�uE�v�ł��ׂ��͂����I�I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "�Ђ����Ă邩�ׂ̂ނ����ɃS�[���������I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        tutorialCanvas.SetActive(false);
        playerController.ChangeCanMove();
    }

    private async void TutorialGoal()
    {
        tutorialCanvas.SetActive(true);
        playerController.ChangeCanMove();
        text.text = "�S�[���I���߂łƂ��������܂��I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "���̂��傤���łނ��������X�e�[�W�ɂ����傤���񂵂悤�I";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        SceneManager.LoadScene("TitleScene");
    }
}
