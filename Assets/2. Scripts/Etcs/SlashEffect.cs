using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private Animator anim;

    private void Start()
    {
        playerStatus = GameObject.FindObjectOfType<PlayerStatus>().GetComponent<PlayerStatus>();
        anim = GetComponent<Animator>();

        anim.speed = playerStatus.animationSpeed;
    }
    private void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
