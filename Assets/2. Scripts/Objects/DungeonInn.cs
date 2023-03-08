using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonInn : MonoBehaviour
{
    private Animator anim;
    private bool isPlayerIn;
    
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isPlayerIn = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !isPlayerIn)
        {
            //자연스럽게 플레이어가 사라지게 하도록 변수 선언
            player = col.gameObject;
            //트리거에 닿으면 플레이어를 멈추기 위함
            //여기서는 플레이어 오브젝트의 중량을 늘려주는 것으로 구현
            col.gameObject.GetComponent<Rigidbody2D>().mass = 100;
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
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
    IEnumerator PlayerTouchTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("isPlayerIn");
        isPlayerIn = true;
    }
}
