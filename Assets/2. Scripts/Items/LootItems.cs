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
        inventory = GameObject.Find("CharacterPanel").transform.Find("Inventory").GetComponent<Inventory>();
        goldText = inventory.transform.GetChild(3).GetComponentInChildren<Text>();
    }

    private void Update()
    {
        StartCoroutine(ItemSpawn());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private IEnumerator ItemSpawn()
    {
        yield return new WaitForSeconds(1.0f);

        if (isInRange)
        {
            if (item != null)
            {
                if(item.itemDescription == "금괴")
                {
                    gold += 100;

                    goldText.text = gold.ToString();
                }

                inventory.AddItem(item);
                item = null;
                Destroy(gameObject);
            }
        }
    }
}