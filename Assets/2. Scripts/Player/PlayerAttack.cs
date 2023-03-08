using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    [Header("플레이어 공격")]
    [SerializeField]
    private bool attacking = false;
    [SerializeField]
    private bool upperSwing = false;
    [SerializeField]
    private float attackDelay = 0.75f;
    [SerializeField]
    private int m_swing = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    private void Attack()
    {
        AnimControl();

        if (attacking)
            return;

        attacking = true;
        anim.SetBool("Attack", true);
        anim.SetInteger("Swing", m_swing);
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        attacking = false;
        anim.SetBool("Attack", false);
    }
    private void AnimControl()
    {
        if(!upperSwing)
        {
            m_swing = 0;
        }
        else
        {
            m_swing = 1;
        }
        upperSwing = !upperSwing;
    }
}
