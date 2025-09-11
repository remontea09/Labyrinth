using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float destroyDelay = 2f;
    public AudioSource explosionSound;
    public AudioClip explosio;
    public ParticleSystem explosionParticles;
    public bool isUse = false;
    public Collider collider;

    public FlagList flagList;
    public FlagData throwFlag;
     void Start()
    {
        if(explosionParticles != null)
        {
            explosionParticles.Stop();
        }
        explosionSound = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Wall")&& flagList.GetFlagStatus(throwFlag))
        {
            print(collision.gameObject.name);
            Destroy(collision.gameObject); // •Ç‚ð”j‰ó
            if(collider != null)
            {
                Destroy(collider);
            }
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            if(explosionSound != null)
            {
                Debug.Log("aaaaaaaaaaaaaa");
                //explosionSound.PlayOneShot(explosio);
                explosionSound.Play();
                Destroy(GetComponent<MeshRenderer>());
            }
            Destroy(explosion, destroyDelay);
            //Destroy(gameObject, destroyDelay);
        }
    }
}
