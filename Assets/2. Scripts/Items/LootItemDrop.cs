using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Accessory,
    Gold
}

[System.Serializable]
public struct LootItem
{
    public Item item;
    public Sprite itemImage;
    [Range(0, 100)]
    public int dropChance;
    public string itemType;
}

public class LootItemDrop : MonoBehaviour
{
    public GameObject lootItemPrefab;                   //아이템 오브젝트 프리팹
    public GameObject lootGoldPrefab;                   //골드 오브젝트 프리팹
    public Sprite changeImage;                          //상자가 열린 모양으로 바꿔주도록
    public Image helpImage;                             //플레이어가 접근했을 때 띄울 키보드 이미지

    public LootItem[] lootItems;
    private bool isBoxOpen;

    public float dropRadius;                            //아이템이 드롭될 반경
    public float upwardForce;                           //아이템 드롭시 위로 가해지는 힘
    public float torque;                                //아이템 드롭시 회전하는 힘

    private Rigidbody2D rigid;
    private BoxCollider2D box;

    private void Start()
    {
        helpImage.gameObject.SetActive(false);
        isBoxOpen = false;
        //여러 변수로 실험해본 결과 원하는 모양새와 비슷하여 Fix함.
        dropRadius = 1.5f;
        upwardForce = 5.0f;
        torque = 5.0f;

        rigid = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && !isBoxOpen)
        {
            helpImage.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F) && !isBoxOpen)
            {
                DropItem();
                //플레이어와 상호작용 이후 열린 이미지로 변경되고 상자가 없어지게 하기 위해
                //1초의 딜레이를 주었다.
                //StartCoroutine(DestroyDropBox());
                box.isTrigger = true;
                rigid.isKinematic= true;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player") && isBoxOpen ||
            col.CompareTag("Player") && !isBoxOpen)
        {
            helpImage.gameObject.SetActive(false);
        }
    }

    IEnumerator DestroyDropBox()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private void DropItem()
    {
        Vector2 dropPosition = transform.position;

        isBoxOpen = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = changeImage;

        //아이템이 생성될 갯수를 랜덤으로 정함
        int numberOfDrops = Random.Range(4, 7);

        for (int i = 0; i < numberOfDrops; i++)
        {
            int randomNumber = Random.Range(0, 100);

            //드롭율에 따라 생성될 아이템 선택
            int dropSum = 0;
            foreach (LootItem item in lootItems)
            {
                dropSum += item.dropChance;
                if (randomNumber < dropSum && item.itemType == ItemType.Accessory.ToString())
                {
                    //아이템의 위치를 랜덤하게 설정
                    GameObject newItem = Instantiate(lootItemPrefab, dropPosition, Quaternion.identity);

                    //생성된 게임 오브젝트에 해당 아이템의 정보가 들어가도록.
                    SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = item.itemImage;

                    LootItems lootItems = newItem.GetComponent<LootItems>();
                    lootItems.item = item.item;

                    newItem.name = lootItems.item.itemName;

                    //아이템에 랜덤한 방향으로 가해질 힘과 회전력을 추가
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector2 force = randomDirection * upwardForce;

                    Rigidbody2D rigid = newItem.GetComponent<Rigidbody2D>();
                    rigid.AddForce(force, ForceMode2D.Impulse);
                    rigid.AddTorque(torque, ForceMode2D.Impulse);

                    //아이템의 위치를 조정하여 붙어서 생성되지 않도록 함.
                    Vector2 offset = Random.insideUnitCircle * dropRadius / 2.0f;
                    newItem.transform.position += new Vector3(offset.x, 0.5f, 0f);

                    break;
                }
                if(randomNumber < dropSum && item.itemType == ItemType.Gold.ToString())
                {
                    //아이템의 위치를 랜덤하게 설정
                    GameObject newItem = Instantiate(lootGoldPrefab, dropPosition, Quaternion.identity);

                    //생성된 게임 오브젝트에 해당 아이템의 정보가 들어가도록.
                    SpriteRenderer spriteRenderer = newItem.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = item.itemImage;

                    LootItems lootItems = newItem.GetComponent<LootItems>();
                    lootItems.item = item.item;

                    newItem.name = lootItems.item.itemName;

                    //아이템에 랜덤한 방향으로 가해질 힘과 회전력을 추가
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;
                    Vector2 force = randomDirection * upwardForce;

                    Rigidbody2D rigid = newItem.GetComponent<Rigidbody2D>();
                    rigid.AddForce(force, ForceMode2D.Impulse);
                    rigid.AddTorque(torque, ForceMode2D.Impulse);

                    //아이템의 위치를 조정하여 붙어서 생성되지 않도록 함.
                    Vector2 offset = Random.insideUnitCircle * dropRadius / 2.0f;
                    newItem.transform.position += new Vector3(offset.x, 0.5f, 0f);

                    break;
                }
            }
        }
    }
}