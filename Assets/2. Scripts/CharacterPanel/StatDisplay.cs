using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat
    {
        get
        {
            return _stat;
        }
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }
    private string _name;
    public string Name
    {
        get
        { 
            return _name;
        }
        set
        {
            _name = value;
            nameText.text = _name;
        }
    }
    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }

    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField] StatToolTip tooltip;

    private void OnValidate()
    {
        Text[] text = GetComponentsInChildren<Text>();

        nameText = text[0];
        valueText = text[1];

        if(tooltip == null)
        {
            tooltip = FindObjectOfType<StatToolTip>();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

}
