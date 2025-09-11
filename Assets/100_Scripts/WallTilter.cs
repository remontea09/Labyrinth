using Cysharp.Threading.Tasks;
using UnityEngine;

public class WallTilter : MonoBehaviour
{
    [Header("•Ç‚ğŒX‚¯‚éŠp“x")]
    [Tooltip("0“x‚ª’¼—§A90“x‚ªŠ®‘S‚É“|‚ê‚½ó‘Ô")]
    [Range(0f, 90f)]
    public float tiltAngle = 0f;

    [Header("ŒX‚«‚Ì‘¬‚³")]
    public float tiltSpeed = 2.0f;

    private Quaternion targetRotation;

    private void WallTail()
    {
        targetRotation = Quaternion.Euler(tiltAngle, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < 9; i++)
            {
                WallTail();
                await UniTask.WaitForSeconds(0.05f);
            }
            GameManager.instance.GetDanage();
            Destroy(this.gameObject);
        }
    }
}