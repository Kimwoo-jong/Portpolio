using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject enemy;                //Enemy 오브젝트
    private Animator anim;                  //애니메이터
    private float health;                   //체력
    private float maxHealth;                //최대 체력

    public GameObject lootBox;              //아이템 박스
    public GameObject deathEffectPrefab;    //사망 이펙트 프리팹
    private GameObject death;               //사망 이펙트


    private void Start()
    {
        //부모 오브젝트만 가지고 있는 컴포넌트를 통해서 접근
        enemy = GetComponentInParent<CapsuleCollider2D>().gameObject;
        anim = GetComponentInParent<Animator>();
        maxHealth = 40f;
        //체력을 최대로 초기화해줌.
        health = maxHealth;
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        SoundManager.instance.EnemyHitSound();
        CheckDeath();
    }
    private void CheckOverheal()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void CheckDeath()
    {
        if(health <= 0)
        {
            //같은 몬스터가 많이 생성된다면 오브젝트 풀링으로 전환할 예정
            StartCoroutine(DeathEffect());
            SoundManager.instance.EnemyDeathSound();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Enemy가 데미지를 입음
        if (col.gameObject.CompareTag("Weapon"))
        {
            DealDamage(20);
            StartCoroutine(DamageEffect());
        }
    }
    private IEnumerator DeathEffect()
    {
        death = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(0.75f);

        Destroy(death);
        Instantiate(lootBox, transform.position, Quaternion.identity);
        Destroy(enemy);
    }

    private IEnumerator DamageEffect()
    {
        anim.SetBool("IdleHit", true);

        yield return new WaitForSeconds(0.35f);

        anim.SetBool("IdleHit", false);
    }
}
