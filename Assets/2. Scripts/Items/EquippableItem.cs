using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    FirstMainWeapon,
    FirstSubWeapon,
    SecondMainWeapon,
    SecondSubWeapon,
    Accessory1,
    Accessory2,
    Accessory3,
    Accessory4,
    Accessory5,
}
public enum WeaponType
{
    한손무기,
    양손무기,
    액세서리
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create Equippable Item")]
public class EquippableItem : Item
{
    [Header("장비 공격 관련")]
    public int m_damageBonus;
    public int m_criticalBonus;
    public int m_criDamageBonus;
    public int m_dashDamageBonus;
    public int m_trueDamageBonus;

    [Header("장비 방어 관련")]
    public int m_defenseBonus;
    public int m_toughBonus;
    public int m_blockBonus;
    public int m_evadeBonus;

    [Header("장비 속도 관련")]
    public float m_atkSpdBonus;
    public float m_moveSpdBonus;
    public float m_reloadSpdBonus;

    [Space]
    public EquipmentType equipmentType;
    public WeaponType weaponType;

    public void Equip(PlayerStatus c)
    {
        #region 상수값
        if (m_damageBonus != 0)
            c.damage.AddModifier(new StatModifier(m_damageBonus, StatModType.Flat, this));
        if (m_defenseBonus != 0)
            c.defense.AddModifier(new StatModifier(m_defenseBonus, StatModType.Flat, this));
        if (m_toughBonus != 0)
            c.tough.AddModifier(new StatModifier(m_toughBonus, StatModType.Flat, this));
        if (m_blockBonus != 0)
            c.block.AddModifier(new StatModifier(m_blockBonus, StatModType.Flat, this));
        if (m_criticalBonus != 0)
            c.critical.AddModifier(new StatModifier(m_criticalBonus, StatModType.Flat, this));
        if (m_criDamageBonus != 0)
            c.criDamage.AddModifier(new StatModifier(m_criDamageBonus, StatModType.Flat, this));
        if (m_evadeBonus != 0)
            c.evade.AddModifier(new StatModifier(m_evadeBonus, StatModType.Flat, this));
        if (m_moveSpdBonus != 0)
            c.moveSpeed.AddModifier(new StatModifier(m_moveSpdBonus, StatModType.Flat, this));
        if (m_atkSpdBonus != 0)
            c.atkSpeed.AddModifier(new StatModifier(m_atkSpdBonus, StatModType.Flat, this));
        if (m_reloadSpdBonus != 0)
            c.reloadSpeed.AddModifier(new StatModifier(m_reloadSpdBonus, StatModType.Flat, this));
        if (m_dashDamageBonus != 0)
            c.dashDamage.AddModifier(new StatModifier(m_dashDamageBonus, StatModType.Flat, this));
        if (m_trueDamageBonus != 0)
            c.trueDamage.AddModifier(new StatModifier(m_trueDamageBonus, StatModType.Flat, this));
        #endregion
    }
    public void UnEquip(PlayerStatus c)
    {
        c.damage.RemoveAllModifiersFromSource(this);
        c.defense.RemoveAllModifiersFromSource(this);
        c.tough.RemoveAllModifiersFromSource(this);
        c.block.RemoveAllModifiersFromSource(this);
        c.critical.RemoveAllModifiersFromSource(this);
        c.criDamage.RemoveAllModifiersFromSource(this);
        c.evade.RemoveAllModifiersFromSource(this);
        c.moveSpeed.RemoveAllModifiersFromSource(this);
        c.atkSpeed.RemoveAllModifiersFromSource(this);
        c.reloadSpeed.RemoveAllModifiersFromSource(this);
        c.dashDamage.RemoveAllModifiersFromSource(this);
        c.trueDamage.RemoveAllModifiersFromSource(this);
    }
    public override string GetItemType()
    {
        return weaponType.ToString();
    }
}
