using UnityEngine;

public class Audio_Play : MonoBehaviour
{
    private AudioSource audioSource; // AudioSource���擾
    bool isAudioPlaying = false;   // �Đ����̔���

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();        // ���ʉ����Đ�
        isAudioPlaying = true;    // �Đ����t���O��ݒ�
    }

    void Update()
    {
        if (!audioSource.isPlaying && isAudioPlaying)
        {
            Destroy(gameObject);  // ���ʉ��Đ���ɃI�u�W�F�N�g���폜
        }
    }
}