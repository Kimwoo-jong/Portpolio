using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Animator bossAnim;                   //본체 애니메이터

    public int index;                           //보스의 패턴을 위한 인덱스

    private void Start()
    {
        bossAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(index == 1)
            {
                LHandAttackState();
            }
            else if(index == 2)
            {
                RHandAttackState();
            }
        }
    }
    private void FixedUpdate()
    {
        index = Random.Range(0, 3);
        Debug.Log("index : " + index);
    }
    public void BossAttackEnd()
    {
        bossAnim.SetBool("Idle", true);
        bossAnim.SetBool("Attack", false);
    }
    private void LHandAttackState()
    {
        bossAnim.SetBool("Idle", false);
        bossAnim.SetBool("Attack", true);
        bossAnim.SetInteger("Index", 1);
    }
    private void RHandAttackState()
    {
        bossAnim.SetBool("Idle", false);
        bossAnim.SetBool("Attack", true);
        bossAnim.SetInteger("Index", 2);
    }
}
