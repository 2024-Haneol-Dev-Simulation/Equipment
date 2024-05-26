using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EnhancementAttribute : UIElement
{
    [Header("System")]
    [SerializeField] private EquipmentSystem equipmentSystem;
    [Header("UI")]
    [SerializeField] private Image TopImage;
    [SerializeField] private Image MidImage;
    [Space(10)]
    [SerializeField] private TMP_Text EquipmentNameText;
    [SerializeField] private TMP_Text EquipmentTypeText;
    [Space(10)]
    [SerializeField] private Image EquipmentImage;
    [Space(10)]
    [SerializeField] private TMP_Text MainOptionTypeText;
    [SerializeField] private TMP_Text MainOptionValueText;
    [Space(10)]
    [SerializeField] private TMP_Text RatingText;
    [Space(10)]
    [SerializeField] private TMP_Text EnhancementCountText;
    [Space(10)]
    [SerializeField] private AdditionalOption additionalOption;
    [Space(10)]
    [SerializeField] private EquipmentSetUI equipmentSetUI;
    [Space(10)]
    [SerializeField] private TMP_Text EquipmentExperienceText;
    [SerializeField] private bool StartToShowInit;
    public void Init(MyEquipment myEquipment = null)
    {
        if(myEquipment == null)
             myEquipment = equipmentSystem.GetEquipment(equipmentSystem.SelectEquipmentID);

        if(myEquipment.equipmentClass == EquipmentClass.Rare)
        {
            TopImage.color = new Color32(161, 86, 224, 255);
            MidImage.color = new Color32(104, 92, 144,255);
        }
        else
        {
            TopImage.color = new Color32(188, 105, 50, 255);
            MidImage.color = new Color32(214, 167, 100, 255);
        }

        EquipmentNameText.text = myEquipment.equipment.name;
        EquipmentTypeText.text = myEquipment.equipment.GetEquipmentType();

        EquipmentImage.sprite = myEquipment.equipment.image;

        MainOptionTypeText.text = myEquipment.mainOption.GetOptionTypeString();
        MainOptionValueText.text = myEquipment.mainOption.GetOptionValueString();

        Rating(myEquipment.equipmentClass);

        EnhancementCountText.text = $"+{myEquipment.enhancement}";

        additionalOption.Init(myEquipment.additionalOption);

        equipmentSetUI.Init(equipmentSystem.GetEquipmentSet(myEquipment.equipment));

        EquipmentExperienceText.text = myEquipment.equipment.experience;
    }
    private void Rating(EquipmentClass equipmentClass)
    {
        switch (equipmentClass)
        {
            case EquipmentClass.Rare:
                RatingText.text = "¡Ú¡Ú¡Ú¡Ú";
                break;
            case EquipmentClass.Legend:
                RatingText.text = "¡Ú¡Ú¡Ú¡Ú¡Ú";
                break;
        }
    }
    public override void Hide(HideType hideType, Complete complete = null)
    {
        if (hideType == HideType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() =>
            {
                if (complete != null)
                    complete();
                gameObject.SetActive(false);
            });
        }
        

        
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        if(StartToShowInit)
            Init();
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
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        }

        if (complete != null)
            complete();
    }
}
