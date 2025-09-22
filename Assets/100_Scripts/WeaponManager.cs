using UnityEngine;

using static Unity.Collections.CollectionHelper;

public class WeaponManager : MonoBehaviour
{
    public GameObject hammer;       
    public GameObject BombPrefab;   
    public Transform handTransform; 
    public GameObject dummyBomb;
    public float forceMagnitude = 10f; // 飛ばす力
    public Vector3 direction = Vector3.forward; // 飛ばす方向 (初期値は前方向)
    private float speed = 300;
    private bool hasBomb = false;

    public FlagList flagList;
    public FlagData throwFlag;
    void Start()
    {
        dummyBomb.SetActive(false); // 最初は見た目爆弾を非表示
        throwFlag.InitFlag();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && hasBomb)
        {
            

            ThrowBomb();
            throwFlag.SetFlagStatus(true);

        }

        //Debug.Log(direction);
    }

    public void PickUpBomb()
    {
        Debug.Log("爆弾を拾った");
        hasBomb = true;
        hammer.SetActive(false); 
        dummyBomb.SetActive(true);
    }

    void ThrowBomb()
    {
        GameObject bomb = Instantiate(BombPrefab, handTransform.position, Quaternion.identity);
        Rigidbody rb = bomb.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);


        hasBomb = false;
        hammer.SetActive(true);
        dummyBomb.SetActive(false);
        throwFlag.InitFlag();

    }
    // Update is called once per frame

}
