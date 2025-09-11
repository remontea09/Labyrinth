using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeText;

    private int time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = (int)TimeKeeper.time;
        timeText.text = time.ToString();
    }
}
