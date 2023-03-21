using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private Rigidbody2D rigid;
    private Animator anim;

    private void Start()
    {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>().GetComponent<PlayerStatus>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rigid.velocity = (Vector2)(transform.right * playerStatus.effectSpeed);
        anim.speed = playerStatus.animationSpeed;
    }
    private void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
