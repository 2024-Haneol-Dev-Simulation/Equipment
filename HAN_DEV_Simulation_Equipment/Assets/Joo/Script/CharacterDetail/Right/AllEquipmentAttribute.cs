using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllEquipmentAttribute : UIElement
{
    [SerializeField] private EquipmentSystem equipmentSystem;

    [Header("UI")]
    [SerializeField] private List<StatElement> statElements;
    [SerializeField] private GameObject SetObjectParent;

    [Header("Prefab")]
    [SerializeField] private GameObject SetUI;
    public void Init()
    {
        Stats stats = equipmentSystem.GetEquipStats();
        statElements[0].Init("HP �ִ�ġ", $"+{stats.HP}");
        statElements[1].Init("���ݷ�", $"+{stats.ATK}");
        statElements[2].Init("����", $"+{stats.DEF}");
        statElements[3].Init("ġ��Ÿ Ȯ��", $"+{stats.CritRate}%");
        statElements[4].Init("ġ��Ÿ ����", $"+{stats.CritDMG}%");
        statElements[5].Init("ġ�����ʽ�", $"+{stats.HealingBonus}%");

        List<MyEquipmentSet> equipmentSets = equipmentSystem.GetAllEquipmentSet();

        for( int i = 0; i < SetObjectParent.transform.childCount; i++ )
        {
            Destroy(SetObjectParent.transform.GetChild(i).gameObject);
        }

        foreach(var  equipmentSet in equipmentSets)
        {
            if (equipmentSet.activeCount <= 1)
                continue;
            GameObject obj = Instantiate(SetUI, SetObjectParent.transform);
            obj.GetComponent<EquipmentSetUI>().Init(equipmentSet);
        }
    }
    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        Init();
        equipmentSystem.SelectEquipmentID = string.Empty;

        gameObject.SetActive(true);
        if (showType == ShowType.FadeAndMove)
        {
            Vector3 pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, pos.y, pos.z);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(pos, 0.3f);
        }
        else if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }
}
