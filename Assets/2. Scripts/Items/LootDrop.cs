using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct LootItem
{
    public Item item;
    public Sprite itemImage;
    [Range(0, 100)]
    public int dropChance;
}

public class LootDrop : MonoBehaviour
{
    public GameObject lootPrefab;
    public Sprite changeImage;
    public Image helpImage;

    public LootItem[] lootItems;
    private bool isBoxOpen;

    private void Start()
    {
        helpImage.gameObject.SetActive(false);
        isBoxOpen = false;
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
                        if (randomNumber < dropSum)
                        {
                            //X축으로 -1 ~ 1만큼 이동하여 랜덤 생성해주기 위함
                            GameObject newItem = Instantiate(lootPrefab, new Vector2(Random.Range(-1f, 1f), transform.position.y - 0.25f), Quaternion.identity);
                            
                            newItem.GetComponent<SpriteRenderer>().sprite = item.itemImage;
                            newItem.GetComponent<LootItems>().item = item.item;

                            //생성된 게임 오브젝트의 이름을 아이템 정보에 있는 이름으로 변경
                            newItem.gameObject.name = newItem.GetComponent<LootItems>().item.itemName;

                            break;
                        }
                    }
                }
                //플레이어와 상호작용 이후 열린 이미지로 변경되고 상자가 없어지게 하기 위해
                //1초의 딜레이를 주었다.
                //StartCoroutine(DestroyDropBox());
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
}