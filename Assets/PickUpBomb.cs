using UnityEngine;

public class PickUpBomb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<WeaponManager>().PickUpBomb();
            Destroy(gameObject);
        }
    }
}
