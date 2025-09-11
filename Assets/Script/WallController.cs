using UnityEngine;

public class WallController : MonoBehaviour
{

    public float speed = 0.3f;
    private Rigidbody rb;
    private bool wall = false;

    void Start()
    {
        // Rigidbody を取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody がアタッチされていません！");
        }
    }
    public  void FixedUpdate()
    {
        if (wall)
        {
            //rb.AddForce(transform.forward * -speed, ForceMode.Force);
            rb.AddForce(-transform.forward * speed, ForceMode.VelocityChange);
        }
    }

    Vector3 direction;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("a");
            wall = true;
        }
        if (other.gameObject.CompareTag("Stop"))
        {
            wall = false;
            rb.linearVelocity = Vector3.zero;
        }
    }

}
