using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(IsFacingRight())
        {
            rigid.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rigid.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    private bool IsFacingRight()
    {
        //Epsilon >> 0에 가까운 소수점을 반환
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigid.velocity.x)), transform.localScale.y);
    }
}
