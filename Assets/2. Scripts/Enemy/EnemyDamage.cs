using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject enemy;                //Enemy 오브젝트
    private Animator anim;                  //애니메이터
    public float health;                   //체력
    public float maxHealth;                //최대 체력
    public bool isHit;                     //맞았는지 판단 (중첩 데미지가 들어가는 것을 방지)

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
        isHit = true;
    }
    public void DealDamage(float damage)
    {
        if(isHit)
        {
            health -= damage;
            SoundManager.instance.EnemyHitSound();
            CheckDeath();
            Debug.Log("10 데미지");
        }
        else
        {
            Debug.Log("무적상태");
            return;
        }
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
            StartCoroutine(DamageEffect());
        }
    }
    private IEnumerator DeathEffect()
    {
        death = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(0.75f);

        Instantiate(lootBox, transform.position, Quaternion.identity);
        Destroy(enemy);
    }

    private IEnumerator DamageEffect()
    {
        anim.SetBool("IdleHit", true);
        isHit = true;
        DealDamage(10);

        yield return new WaitForSeconds(0.35f);

        anim.SetBool("IdleHit", false);
        isHit = false;
    }
}
