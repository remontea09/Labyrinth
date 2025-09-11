using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float swingAngle = 130f;       // 振る角度
    public float swingSpeed = 5f;        // 振る速さ
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private float swingProgress = 0f;
    public int hammerCount;


    public FlagList flagList;
    public FlagData swingFlag;
    private AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalRotation = transform.localRotation;
        hammerCount = GameManager.instance.hammerCount;
        swingFlag.InitFlag();
    }

    void Update()
    {
        hammerCount = GameManager.instance.hammerCount;
        // Dキーでアクション開始
        if (Input.GetKeyDown(KeyCode.E) && flagList.GetFlagStatus(swingFlag) == false && hammerCount !=0)
        {
            audioSource.clip = clip;
            audioSource.Play();
            swingFlag.SetFlagStatus(true);
            swingProgress = 0f;

            // 振る方向（X軸を基準に前に振る）
            targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(swingAngle, 0f, 0f));
            hammerCount = GameManager.instance.HammerActionCount();
        }

        // アクション中の動き
        if (flagList.GetFlagStatus(swingFlag))
        {
            swingProgress += Time.deltaTime * swingSpeed;

            // 徐々に回転 → 完了後、元に戻す
            transform.localRotation = Quaternion.Slerp(originalRotation, targetRotation, swingProgress);

            if (swingProgress >= 1f)
            {
                // 戻す
                StartCoroutine(RestoreAfterDelay(0.1f));
                swingFlag.InitFlag();
            }
        }
    }
    //public 

    // 元の角度に戻す処理（短いディレイつき）
    System.Collections.IEnumerator RestoreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Slerp(targetRotation, originalRotation, t);
            yield return null;
        }

        transform.localRotation = originalRotation;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall")&& flagList.GetFlagStatus(swingFlag) == true)
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
