﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDungeonInn : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerIn;

    [SerializeField] private GameObject player;                      //플레이어
    [SerializeField] private GameObject DungeonEnter;                //던전 입구
    [SerializeField] private Vector3 innSpawnPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isPlayerIn = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && !isPlayerIn)
        {
            SpawnDungeon(col);
            col.gameObject.transform.position = new Vector3(innSpawnPoint.x, 0f, innSpawnPoint.z);
        }
    }
    private void SpawnDungeon(Collision2D col)
    {
        isPlayerIn = true;
        //첫 번째 충돌지점
        ContactPoint2D contact = col.contacts[0];

        GameObject Inn = Instantiate(DungeonEnter, new Vector2(contact.point.x, 1.3f), Quaternion.identity);

        innSpawnPoint = Inn.transform.position;
    }
}
