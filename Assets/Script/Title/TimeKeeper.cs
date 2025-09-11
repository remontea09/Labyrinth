using TMPro;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeText;

    public static float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int intTime = (int)time;
        timeText.text = intTime.ToString();
    }
}
