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
        statElements[0].Init("HP �ִ�ġ", $"{stats.HP}");
        statElements[1].Init("���ݷ�", $"{stats.ATK}");
        statElements[2].Init("����", $"{stats.DEF}");
        statElements[3].Init("ġ��Ÿ Ȯ��", $"{stats.CritRate}%");
        statElements[4].Init("ġ��Ÿ ����", $"{stats.CritDMG}%");
        statElements[5].Init("ġ�����ʽ�", $"{stats.HealingBonus}%");
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
