using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus playerStatus;
    public GameObject player;
    
    [SerializeField] StatPanel statPanel;

    public float effectSpeed;               //이펙트 속도
    public float animationSpeed;            //이펙트 애니메이션 재생 속도

    [Header("Level")]
    [SerializeField] private Text m_Level;                   //플레이어의 레벨 텍스트
    [SerializeField] private int playerLevel;                //플레이어의 레벨

    [Header("Stats")]
    public CharacterStat damage;
    public CharacterStat defense;
    public CharacterStat tough;
    public CharacterStat block;
    public CharacterStat critical;
    public CharacterStat criDamage;
    public CharacterStat evade;
    public CharacterStat moveSpeed;
    public CharacterStat atkSpeed;
    public CharacterStat reloadSpeed;
    public CharacterStat dashDamage;
    public CharacterStat trueDamage;

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
        
        statPanel.SetStats(damage, defense, tough, block, critical, criDamage,
            evade, moveSpeed, atkSpeed, reloadSpeed,dashDamage, trueDamage);
        statPanel.UpdateStatsValues();
    }
    private void Update()
    {
        DisplayLevel(playerLevel);
        statPanel.UpdateStatsValues();
    }
    public void DisplayLevel(int level)
    {
        m_Level.text = level.ToString();
    }
}
