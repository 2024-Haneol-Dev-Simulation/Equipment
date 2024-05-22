using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EquipmentAttribute : UIElement
{
    [Header("System")]
    [SerializeField] private EquipmentSystem equipmentSystem;
    [Header("UI")]
    [SerializeField] private TMP_Text EquipmentNameText;
    [SerializeField] private TMP_Text EquipmentTypeText;
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

    [SerializeField] private Button equipOrUnequipButton;
    [SerializeField] private TMP_Text equipOrUnequipText;
    public void Init(MyEquipment myEquipment)
    {
        EquipmentNameText.text = myEquipment.equipment.name;
        EquipmentTypeText.text = myEquipment.equipment.GetEquipmentType();

        MainOptionTypeText.text = myEquipment.mainOption.GetOptionTypeString();
        MainOptionValueText.text = myEquipment.mainOption.GetOptionValueString();

        Rating(myEquipment.equipmentClass);

        EnhancementCountText.text = $"+{myEquipment.enhancement}";

        additionalOption.Init(myEquipment.additionalOption);

        equipmentSetUI.Init(equipmentSystem.GetEquipmentSet(myEquipment.equipment));

        EquipmentExperienceText.text = myEquipment.equipment.experience;

        //ÀåÂø ÇØÁ¦
        equipOrUnequipButton.onClick.RemoveAllListeners();
        if (equipmentSystem.GetEquipEquipment(myEquipment.equipment.GetType()) == myEquipment)
        {
            equipOrUnequipText.text = "ÇØÁ¦"; 
            equipOrUnequipButton.onClick.AddListener(() => { 
                equipmentSystem.RemoveEquipEquipment(myEquipment.equipment.GetType());
                Init(myEquipment);
            });
        }
        else
        {
            equipOrUnequipText.text = "ÀåÂø";
            equipOrUnequipButton.onClick.AddListener(() => { 
                equipmentSystem.SetEquipEquipment(myEquipment);
                Init(myEquipment);
            });
        }


    }
    private void Rating(EquipmentClass equipmentClass)
    {
        switch (equipmentClass)
        {
            case EquipmentClass.Nomal:
                RatingText.text = "¡Ú¡Ú¡Ú";
                break;
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
        gameObject.SetActive(false);

        if (complete != null)
            complete();
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        gameObject.SetActive(true);
        if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }

        if (complete != null)
            complete();
    }
}
