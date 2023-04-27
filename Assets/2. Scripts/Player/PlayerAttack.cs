using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerHealth playerHealth;

    [Header("플레이어 공격")]
    [SerializeField] private bool attacking = false;
    [SerializeField] private bool upperSwing = false;
    [SerializeField] private float attackDelay;
    [SerializeField] private int m_swing = 0;

    [Header("공격 이펙트")]
    public Transform firePos;
    public GameObject slashEffect;

    public GameObject weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<BoxCollider2D>().gameObject;
        playerHealth = GetComponentInParent<PlayerHealth>();
        attackDelay = 0.25f;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (attacking)
        {
            return;
        }

        AnimControl();
        SoundManager.instance.PlayerAttackSound();
        StartCoroutine(CorAttack());
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
    //애니메이션에서 Add Event를 통해 공격 모션 실행시 생성되도록 한다.
    private void MakeEffect()
    {
        //이펙트의 생성은 공격과 동시에
        Instantiate(slashEffect, firePos.position, firePos.rotation);
    }
    private IEnumerator CorAttack()
    {
        attacking = true;
        anim.SetBool("Attack", true);
        anim.SetInteger("Swing", m_swing);

        yield return new WaitForSeconds(attackDelay);

        attacking = false;
        anim.SetBool("Attack", false);
    }
}
