using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private AudioSource audiosource;

    public static MusicManager instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();
    }
}
