using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnewayPlatform : MonoBehaviour
{
    private GameObject currentOnewayPlatform;

    [SerializeField] private BoxCollider2D player;

    private void Start()
    {
        player = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOnewayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Platform"))
        {
            currentOnewayPlatform = col.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Platform"))
        {
            currentOnewayPlatform = null;
        }    
    }
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOnewayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(player, platformCollider, true);

        yield return new WaitForSeconds(0.25f);

        Physics2D.IgnoreCollision(player, platformCollider, false);
    }
}
