
using UnityEngine;

[RequireComponent(typeof(Rigidbody), (typeof(BoxCollider)), (typeof(BoxCollider)))]
public class WallMoveController : MonoBehaviour
{
    //�Ԃ���ق���RigidBody�����Ă��Ȃ��Ɛ���ɓ����Ȃ��ł�
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

        // ��Q�����Ȃ��ꍇ�A�ő勗���Ƃ���
        return maxRayDistance;
    }


    private void OnCollisionEnter(Collision other)
    {

        //print(other.gameObject.tag);

        if (other.gameObject.CompareTag("Hammer") && flagList.GetFlagStatus(swingFlag) == true)//�Ԃ������̂�Hammer���~���铮����󂯎������
        {

            var playerParent = other.collider.gameObject.transform.parent;
            var directionZ = playerParent.position.z;
            var myTransformZ = this.gameObject.transform.position.z;

            var directionX = playerParent.position.x;
            var myTransfromX = this.gameObject.transform.position.x;

            var wallAngle = gameObject.transform.eulerAngles.y;

            if (wallAngle == 90)
            {
                if (myTransfromX - directionX > 0)//������������
                {
                    speedWall = -5f;//�������x
                }
                else
                {
                    speedWall = 5f;//�������x
                }
            }

            else if (wallAngle == 0)
            {
                if (myTransformZ - directionZ > 0)//������������
                {
                    speedWall = -5f;//�������x
                }
                else
                {
                    speedWall = 5f;//�������x
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
            rbWall.linearVelocity = Vector3.zero;//�������~�߂�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            rbWall.linearVelocity = Vector3.zero;
            // �v���C���[���͈͓��ɂ��邩�`�F�b�N
            Vector3 boxCenter = transform.position;
            Vector3 boxSize = new Vector3(3f, 4f, 0.5f);

            // �� �͈̓T�C�Y�𒲐�

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
                    GameManager.instance.GetDanage();  // �� �v���C���[�������̂Ń_���[�W����
                    break;
                }
            }

            Instantiate(audioPrefab, transform.position, transform.rotation); // ���ʉ�Prefab�𐶐�
            Destroy(gameObject);//�ǂƕǂ��Ԃ����������
        }

    }



}
