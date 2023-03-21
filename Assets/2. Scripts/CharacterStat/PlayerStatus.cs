using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus playerStatus;
    public GameObject player;

    public float effectSpeed;               //이펙트 속도
    public float animationSpeed;            //이펙트 애니메이션 재생 속도

    [Header("플레이어의 레벨")]
    [SerializeField] private Text m_Level;                   //플레이어의 레벨 텍스트
    [SerializeField] private int playerLevel;                //플레이어의 레벨

    private void Awake()
    {
        if(playerStatus != null)
        {
            Destroy(playerStatus);
        }
        else
        {
            playerStatus = this;
        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        player = this.gameObject;

        //검을 휘두를 때의 이펙트가 재생되는 속도를 조절함.
        effectSpeed = 1;
        animationSpeed = 0.75f;

        m_Level = GameObject.Find("Level").GetComponent<Text>();
        playerLevel = 1;
    }
    private void Update()
    {
        DisplayLevel(playerLevel);
    }
    public void DisplayLevel(int level)
    {
        m_Level.text = level.ToString();
    }
}
