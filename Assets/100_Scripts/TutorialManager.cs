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
        text.text = "こんにちは！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "このゲームのチュートリアルをせつめいします！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "そうさはかんたん！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "WASDでいどう！「E」でかべをはかい！！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "ひかってるかべのむこうにゴールがあるよ！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        tutorialCanvas.SetActive(false);
        playerController.ChangeCanMove();
    }

    private async void TutorialGoal()
    {
        tutorialCanvas.SetActive(true);
        playerController.ChangeCanMove();
        text.text = "ゴール！おめでとうございます！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        text.text = "このちょうしでむずかしいステージにもちょうせんしよう！";
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        SceneManager.LoadScene("TitleScene");
    }
}
