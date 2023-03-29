using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatToolTip : MonoBehaviour
{
    [SerializeField] Text statNameText;
    [SerializeField] Text statModifierText;
    [SerializeField] Text statDescription;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statName, string statdescription)
    {
        statNameText.text = GetStatsName(stat, statName);
        statModifierText.text = GetStatModifiersText(stat);
        statDescription.text = GetStatDescription(statdescription);
         
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
            if (sb.Length > 0)
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
    //방어, 막기, 크리티컬 확률, 회피를 제외한 나머지 스탯은 추가 텍스트가 뜨지 않는다.
    private string GetStatDescription(string description)
    {
        sb.Length = 0;

        sb.Append(description);

        return sb.ToString();
    }
}
