using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatToolTip : MonoBehaviour
{
    [SerializeField] Text statNameText;
    [SerializeField] Text statDescription;
    [SerializeField] Text statModifierText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName)
    {
        statNameText.text = GetStatsName(stat, statName);
        statModifierText.text = GetStatModifiersText(stat);
        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    private string GetStatsName(CharacterStat stat, string statName)
    {
        sb.Length = 0;
        sb.Append(statName);

        return sb.ToString();
    }
    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;

        foreach (StatModifier mod in stat.StatModifiers)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (mod.Value > 0)
            {
                sb.Append("+");
            }
            sb.Append(mod.Value);

            EquippableItem item = mod.Source as EquippableItem;

            if (item == null)
            {
                Debug.LogError("Modifier is not an EquippableItem!");
            }
        }

        return sb.ToString();
    }
    //스탯에 관한 설명란
    //private string GetStatDescription(CharacterStat stat)
    //{
    //    sb.Length = 0;

    //    foreach (StatModifier mod in stat.StatModifiers)
    //    {
    //        if (sb.Length > 0)
    //        {
    //            sb.AppendLine();
    //        }

    //        EquippableItem item = mod.Source as EquippableItem;

    //        if (item != null)
    //        {
    //            sb.Append(" ");
    //            sb.Append(item.itemDescription);
    //        }
    //        else
    //        {
    //            Debug.LogError("Modifier is not an EquippableItem!");
    //        }
    //    }

    //    return sb.ToString();
    //}
}
