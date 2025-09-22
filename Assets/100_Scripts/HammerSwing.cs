using UnityEngine;

public class HammerSwing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float swingAngle = 130f;       // �U��p�x
    public float swingSpeed = 5f;        // �U�鑬��
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
        // D�L�[�ŃA�N�V�����J�n
        if (Input.GetKeyDown(KeyCode.E) && flagList.GetFlagStatus(swingFlag) == false && hammerCount !=0)
        {
            audioSource.clip = clip;
            audioSource.Play();
            swingFlag.SetFlagStatus(true);
            swingProgress = 0f;

            // �U������iX������ɑO�ɐU��j
            targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(swingAngle, 0f, 0f));
            hammerCount = GameManager.instance.HammerActionCount();
        }

        // �A�N�V�������̓���
        if (flagList.GetFlagStatus(swingFlag))
        {
            swingProgress += Time.deltaTime * swingSpeed;

            // ���X�ɉ�] �� ������A���ɖ߂�
            transform.localRotation = Quaternion.Slerp(originalRotation, targetRotation, swingProgress);

            if (swingProgress >= 1f)
            {
                // �߂�
                StartCoroutine(RestoreAfterDelay(0.1f));
                swingFlag.InitFlag();
            }
        }
    }
    //public 

    // ���̊p�x�ɖ߂������i�Z���f�B���C���j
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
