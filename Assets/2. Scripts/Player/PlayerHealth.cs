using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    
    public Slider healthSlider;             //플레이어 체력바 슬라이더
    public Text healthText;                 //플레이어 체력 텍스트
    public GameObject deathEffect;          //사망 이펙트

    [Header("플레이어 체력")]
    public float health;                    //체력
    public float maxHealth;                 //최대 체력

    private void Start()
    {
        maxHealth = 80;
        //체력을 최대로 초기화해줌.
        health = maxHealth;
    }
    private void Update()
    {
        healthSlider.value = health / maxHealth;
        healthText.text = health + " / " + maxHealth;
    }
    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }
    public void HealPlayer(float heal)
    {
        health += heal;
        CheckOverheal();
    }
    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void CheckDeath()
    {
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(SpawnDeathEffect());
        }
    }
    private IEnumerator SpawnDeathEffect()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        SoundManager.instance.EnemyDeathSound();

        yield return new WaitForSeconds(0.25f);

        CanvasManager.instance.pnlDead.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //플레이어가 데미지를 입음
        if (col.gameObject.CompareTag("Skull"))
        {
            DealDamage(30);
        }
    }
}
