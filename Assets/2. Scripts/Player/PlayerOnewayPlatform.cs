using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnewayPlatform : MonoBehaviour
{
    [SerializeField] private GameObject currentOnewayPlatform;
    [SerializeField] private CapsuleCollider2D player;

    private void Start()
    {
        player = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (currentOnewayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            currentOnewayPlatform = col.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            currentOnewayPlatform = null;
        }
    }
    private IEnumerator DisableCollision()
    {
        Collider2D platformCollider = currentOnewayPlatform.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(player, platformCollider, true);

        yield return new WaitForSeconds(0.25f);

        Physics2D.IgnoreCollision(player, platformCollider, false);
    }
}
