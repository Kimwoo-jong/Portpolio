using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    private static PlayerStatus playerStatus;

    [SerializeField] Text itemName;                      //아이템 이름
    [SerializeField] Text itemValue;                     //아이템 등급
    [SerializeField] Text itemType;                      //무기 타입
    [SerializeField] Text itemDescription;               //아이템 설명
    [SerializeField] Text itemDamage;                    //아이템 공격력
    [SerializeField] Text itemAtkSpeed;                  //아이템 공격속도
    [SerializeField] Text otherItemStats;                //아이템의 나머지 스탯
    [SerializeField] Text itemEquippableCheck;           //장착, 장착해제 텍스트

    [SerializeField] Image itemImage;                    //아이템의 스프라이트

    private StringBuilder sb = new StringBuilder();      //장비의 스탯을 보여주기 위한 StringBuilder

    public void ShowToolTip(EquippableItem item)
    {
        //StringBuilder 초기화
        sb.Length = 0;

        itemName.text = item.itemName;
        itemValue.text = item.m_ItemValue.ToString();
        itemType.text = item.GetItemType();
        itemDescription.text = item.itemDescription;

        itemImage.sprite = item.itemImage;

        AddStat(item.m_damageBonus);
        itemDamage.text = sb.ToString();
        AddStat(item.m_atkSpdBonus);
        itemAtkSpeed.text = sb.ToString();

        sb.Length = 0;                          //나머지 스탯을 표기하기 위해 StringBuilder 초기화

        AddStat(item.m_defenseBonus, "방어력");
        AddStat(item.m_toughBonus, "강인함");
        AddStat(item.m_blockBonus, "막기");
        AddStat(item.m_criticalBonus, "크리티컬");
        AddStat(item.m_criDamageBonus, "크리티컬 데미지");
        AddStat(item.m_evadeBonus, "회피");
        AddStat(item.m_moveSpdBonus, "이동 속도");
        AddStat(item.m_reloadSpdBonus, "재장전 속도");
        AddStat(item.m_dashDamageBonus, "대쉬 공격력");
        AddStat(item.m_trueDamageBonus, "고정 데미지");

        otherItemStats.text = sb.ToString();

        ItemNameColor(item);

        gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    private void AddStat(float value)
    {
        sb.Length = 0;

        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            sb.Append(value);
        }
    }
    private void AddStat(float value, string statName)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
            {
                sb.Append("+");
                sb.Append(value);
            }

            sb.Append(" ");
            sb.Append(statName);
        }
    }
    //아이템 등급에 따른 이름 색상 변경(흰, 파, 초, 노, 빨)
    private void ItemNameColor(EquippableItem item)
    {
        //아이템 등급 일반
        if(item.m_ItemValue == ItemValue.일반)
        {
            itemName.color = new Color(1, 1, 1, 1);
        }
        //아이템 등급 고급
        if (item.m_ItemValue == ItemValue.고급)
        {
            itemName.color = new Color(0, 0, 1, 1);
        }
        //아이템 등급 영웅
        if (item.m_ItemValue == ItemValue.영웅)
        {
            itemName.color = new Color(0, 1, 0, 1);
        }
        //아이템 등급 희귀
        if (item.m_ItemValue == ItemValue.희귀)
        {
            itemName.color = new Color(1, 1, 0, 1);
        }
        //아이템 등급 전설
        if (item.m_ItemValue == ItemValue.전설)
        {
            itemName.color = new Color(1, 0, 0, 1);
        }
    }
}
