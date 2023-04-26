using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonInn : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerIn;

    [SerializeField] private GameObject player;                      //플레이어

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        isPlayerIn = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !isPlayerIn)
        {
            StartCoroutine("PlayerTouchTrigger");
        }
    }
    private void GoNextScene()
    {
        StartCoroutine("AnimEndLoadScene");
    }
    private void PlayerDisappear()
    {
        player.SetActive(false);
    }
    IEnumerator AnimEndLoadScene()
    {
        SceneManager.LoadScene(3);
        player.SetActive(true);
        yield return null;
    }
    IEnumerator PlayerTouchTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("isPlayerIn");
        isPlayerIn = true;
    }
}