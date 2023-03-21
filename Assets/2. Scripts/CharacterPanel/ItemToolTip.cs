using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] Text itemName;                      //아이템 이름
    [SerializeField] Text itemValue;                     //아이템 등급
    [SerializeField] Text itemType;                      //무기 타입
    [SerializeField] Text itemDescription;               //아이템 설명
    [SerializeField] Text itemDamage;                    //아이템 공격력
    [SerializeField] Text itemAtkSpeed;                  //아이템 공격속도
    [SerializeField] Text otherItemStats;                //아이템의 나머지 스탯
    [SerializeField] Text itemEquippableCheck;           //장착, 장착해제 텍스트

    [SerializeField] Image itemImage;                    //아이템의 스프라이트

    private StringBuilder sbOther = new StringBuilder();      //장비의 다른 스탯들을 보여주기 위한 StringBuilder
    private StringBuilder sbDmg = new StringBuilder();        //장비 공격력
    private StringBuilder sbAtkSpeed = new StringBuilder();   //장비 공격속도

    public void ShowToolTip(EquippableItem item)
    {
        //StringBuilder 초기화
        sbOther.Length = 0;
        sbDmg.Length = 0;
        sbAtkSpeed.Length = 0;

        itemName.text = item.itemName;
        itemValue.text = item.m_ItemValue.ToString();
        itemType.text = item.GetItemType();
        itemDescription.text = item.itemDescription;

        itemImage.sprite = item.itemImage;


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

        AddStat(item.m_defensePercentBonus, "방어력", isPercent: true);
        AddStat(item.m_toughPercentBonus, "강인함", isPercent: true);
        AddStat(item.m_blockPercentBonus, "막기", isPercent: true);
        AddStat(item.m_criticalPercentBonus, "크리티컬", isPercent: true);
        AddStat(item.m_criDamagePercentBonus, "크리티컬 데미지", isPercent: true);
        AddStat(item.m_evadePercentBonus, "회피", isPercent: true);
        AddStat(item.m_moveSpdPercentBonus, "이동 속도", isPercent: true);
        AddStat(item.m_reloadSpdPercentBonus, "재장전 속도", isPercent: true);
        AddStat(item.m_dashDamagePercentBonus, "대쉬 공격력", isPercent: true);
        AddStat(item.m_trueDamagePercentBonus, "고정 데미지", isPercent: true);

        AddDamageStat(item.m_damageBonus);
        AddDamageStat(item.m_damagePercentBonus, isPercent: true);

        AddSpeedStat(item.m_atkSpdBonus);
        AddSpeedStat(item.m_atkSpdPercentBonus, isPercent: true);

        otherItemStats.text = sbOther.ToString();
        itemDamage.text = sbDmg.ToString();
        itemAtkSpeed.text = sbAtkSpeed.ToString();

        ItemNameColor(item);

        gameObject.SetActive(true);
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            if (sbOther.Length > 0)
                sbOther.AppendLine();

            if (value > 0)
                sbOther.Append("+");

            if(isPercent)
            {
                sbOther.Append(value * 100);
                sbOther.Append("% ");
            }
            else
            {
                sbOther.Append(value);
                sbOther.Append(" ");

            }
            sbOther.Append(statName);
        }
    }
    //아이템의 공격력을 연산
    private void AddDamageStat(float value, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sbDmg.Length > 0)
                sbDmg.AppendLine();

            if (isPercent)
            {
                sbDmg.Append(value * 100);
                sbDmg.Append("% ");
            }
            else
            {
                sbDmg.Append(value);
                sbDmg.Append(" ");
            }
        }
    }
    //아이템의 공격속도를 연산
    private void AddSpeedStat(float value, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sbAtkSpeed.Length > 0)
                sbAtkSpeed.AppendLine();

            if (isPercent)
            {
                sbAtkSpeed.Append(value * 1);
                sbAtkSpeed.Append(" ");
            }
            else
            {
                sbAtkSpeed.Append(value);
                sbAtkSpeed.Append(" ");
            }
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
