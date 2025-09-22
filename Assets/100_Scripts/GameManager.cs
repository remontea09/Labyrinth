using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Camera mainCamera;
    public static GameManager instance;
    [Header("プレイヤー")]
    [SerializeField] GameObject player;           
    [SerializeField] GameObject playerSpawn;
    GameObject playerPrefab;
    [Header("UI")]
    [SerializeField] public GameObject timer;
    float time = 0;
    Text timerText;
    [Header("ライフ")]
    public List<GameObject> heartObject = new List<GameObject>();
    int life;
    [Header("ハンマー")]
    public GameObject hammer1;
    public GameObject hammer2;
    public GameObject hammer3;
    public int hammerCount = 3;


    private void Awake()
    {
        if (instance == null)
        {
            
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        playerPrefab = Instantiate(player, playerSpawn.transform.position, Quaternion.Euler(0, 180, 0));
    }
    void Start()
    {
        timerText = timer.GetComponent<Text>();
        
        life = heartObject.Count;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = time.ToString("00");
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Retry();
        }
        if (heartObject.Count == 0)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
    public void GetDanage()
    {
        print(heartObject.Count);

        for (int i = heartObject.Count; i>0; i--)
        {
            Destroy(heartObject[i - 1]);
            heartObject.RemoveAt(i-1);
            
            break;
        }
    }
    public int HammerActionCount()
    {
        switch (hammerCount)
        {
            case 1:
                hammer1.SetActive(false);
                break;
            case 2:
                hammer2.SetActive(false);
                break;
            case 3:
                hammer3.SetActive(false);
                break;
        }
        hammerCount--;



        return hammerCount;
    }
    public void OnHitHammerObject()
    {
        hammerCount = 3;

        hammer1.SetActive(true);
        hammer2.SetActive(true);
        hammer3.SetActive(true);

    }
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
