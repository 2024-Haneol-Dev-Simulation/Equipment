using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : UIElement
{
    [Header("System")]
    [SerializeField] private EquipmentSystem equipmentSystem;
    [Header("UI")]
    [SerializeField] private List<StatElement> statElements;
    public void Init()
    {
        Stats stats = equipmentSystem.GetAllStats();
        statElements[0].Init("HP 최대치", $"{stats.HP}");
        statElements[1].Init("공격력", $"{stats.ATK}");
        statElements[2].Init("방어력", $"{stats.DEF}");
        statElements[3].Init("치명타 확률", $"{stats.CritRate}%");
        statElements[4].Init("치명타 피해", $"{stats.CritDMG}%");
        statElements[5].Init("치유보너스", $"{stats.HealingBonus}%");
    }

    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        Init();

        gameObject.SetActive(true);
        if (showType == ShowType.FadeAndMove)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 55.45f, 0);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-14, 55.45f, 0), 0.3f);
        }
        
    }
}
