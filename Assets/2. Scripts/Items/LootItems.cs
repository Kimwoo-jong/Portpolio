using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootItems : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Text goldText;

    public Item item;
    public int gold;

    private bool isInRange;

    private void Start()
    {
        inventory = GameObject.Find("CharacterPanel").transform.Find("Inventory").GetComponentInChildren<Inventory>();
        goldText = inventory.transform.GetChild(3).GetComponentInChildren<Text>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            TriggerOn();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            ItemPickUp();
        }
    }

    private void TriggerOn()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        box.isTrigger = true;
    }

    private void ItemPickUp()
    {
        if (isInRange)
        {
            if (item != null)
            {
                inventory.AddItem(item);
                item = null;
                Destroy(gameObject);
            }
        }
    }
}