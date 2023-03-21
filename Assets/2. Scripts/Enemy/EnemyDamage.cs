using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float health;                   //체력
    [SerializeField] private float maxHealth;                //최대 체력

    private void Start()
    {
        //체력을 최대로 초기화해줌.
        health = maxHealth;
    }
    public void DealDamage(float damage)
    {
        health -= damage;
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
            Destroy(gameObject);
        }
    }
}
