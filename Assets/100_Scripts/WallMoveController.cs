
using UnityEngine;

[RequireComponent(typeof(Rigidbody), (typeof(BoxCollider)), (typeof(BoxCollider)))]
public class WallMoveController : MonoBehaviour
{
    //ぶつかるほうにRigidBodyがついていないと正常に動かないです
    public LayerMask m_LayerMask;

    public float baseSpeed = 10f;
    public float maxRayDistance = 10f;
    public float speedWall;
    public LayerMask raycastMask;

    private Rigidbody rbWall;
    private bool wallMove = false;

    [SerializeField] FlagList flagList;
    [SerializeField] FlagData swingFlag;

    public AudioSource moveAudioSource;
    public GameObject audioPrefab;
    public void Start()
    {
        rbWall = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        if (wallMove)
        {
            float distance = GetRaycastDistance();
            float adjustedSpeed = Mathf.Clamp(distance, 5f, maxRayDistance) * (speedWall > 0 ? 1 : -1) * baseSpeed;
            print(distance);
            transform.Translate(0, 0, adjustedSpeed * Time.fixedDeltaTime);
        }
    }
    private float GetRaycastDistance()
    {
        RaycastHit hit;
        Vector3 direction = transform.forward * Mathf.Sign(speedWall);

        if (Physics.Raycast(transform.position, direction, out hit, maxRayDistance, raycastMask))
        {
            return hit.distance;
        }

        // 障害物がない場合、最大距離とする
        return maxRayDistance;
    }


    private void OnCollisionEnter(Collision other)
    {

        //print(other.gameObject.tag);

        if (other.gameObject.CompareTag("Hammer") && flagList.GetFlagStatus(swingFlag) == true)//ぶつかったのがHammerかつ降られる動作を受け取ったら
        {

            var playerParent = other.collider.gameObject.transform.parent;
            var directionZ = playerParent.position.z;
            var myTransformZ = this.gameObject.transform.position.z;

            var directionX = playerParent.position.x;
            var myTransfromX = this.gameObject.transform.position.x;

            var wallAngle = gameObject.transform.eulerAngles.y;

            if (wallAngle == 90)
            {
                if (myTransfromX - directionX > 0)//動く向き制御
                {
                    speedWall = -5f;//動く速度
                }
                else
                {
                    speedWall = 5f;//動く速度
                }
            }

            else if (wallAngle == 0)
            {
                if (myTransformZ - directionZ > 0)//動く向き制御
                {
                    speedWall = -5f;//動く速度
                }
                else
                {
                    speedWall = 5f;//動く速度
                }
            }



            if (moveAudioSource != null)
            {
                moveAudioSource.Play();
            }

            wallMove = true;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            wallMove = false;
            rbWall.linearVelocity = Vector3.zero;//動きを止める
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            rbWall.linearVelocity = Vector3.zero;
            // プレイヤーが範囲内にいるかチェック
            Vector3 boxCenter = transform.position;
            Vector3 boxSize = new Vector3(3f, 4f, 0.5f);

            // ← 範囲サイズを調整

            Collider[] hits = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);
            //GameObject[] hitsObjec = new GameObject[hits.Length];
            //for (int i=0; i<hits.Length ; i++)
            //{
            //    GameObject hitObject = hits[i].gameObject;
            //    if (hitObject.CompareTag("Hammer"))
            //    {
            //        hitObject = hitObject.transform.parent.parent.gameObject;
            //    }
            //    hitsObjec[i] = hitObject;
            //}

            foreach (Collider col in hits)
            {
                print(col.gameObject.name);
                if (col.CompareTag("Player"))
                {
                    GameManager.instance.GetDanage();  // ← プレイヤーがいたのでダメージ処理
                    break;
                }
            }

            Instantiate(audioPrefab, transform.position, transform.rotation); // 効果音Prefabを生成
            Destroy(gameObject);//壁と壁がぶつかったら消す
        }

    }



}
