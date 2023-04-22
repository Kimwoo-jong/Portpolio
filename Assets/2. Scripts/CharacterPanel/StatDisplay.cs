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
            UpdateStatName();
        }
    }

    private string _description;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
            UpdateStatDesc();
        }
    }
    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }
    public void UpdateStatName()
    {
        nameText.text = _name;
    }
    public void UpdateStatDesc()
    {
        descriptText.text = _description;
    }

    [SerializeField] Image statImage;
    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField] Text descriptText;
    [SerializeField] StatToolTip tooltip;

    private void OnValidate()
    {
        statImage = GetComponentInChildren<Image>();

        Text[] text = GetComponentsInChildren<Text>();

        nameText = text[0];
        valueText = text[1];
        descriptText = text[2];

        if(tooltip == null)
        {
            tooltip = FindObjectOfType<StatToolTip>();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name, Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

}
