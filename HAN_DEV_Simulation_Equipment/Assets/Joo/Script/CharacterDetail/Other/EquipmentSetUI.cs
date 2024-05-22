using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSetUI : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private bool showSetCount;
    [SerializeField] private bool showAllSetEffect;
    [Header("UI")]
    [SerializeField] private TMP_Text equipmentSetNameText;
    [Space(10)]
    [SerializeField] private TMP_Text set2Text;
    [SerializeField] private Image set2Image;
    [Space(5)]
    [SerializeField] private TMP_Text set4Text;
    [SerializeField] private Image set4Image;
    [Space(10)]
    [SerializeField] private GameObject set2Object;
    [SerializeField] private GameObject set4Object;

    public void Init(MyEquipmentSet myEquipmentSet)
    {
        equipmentSetNameText.text = myEquipmentSet.equipmentSet.setName + ":";
        if (showSetCount)
            equipmentSetNameText.text += $"({myEquipmentSet.activeCount})";

        set2Text.text = $"2세트: {myEquipmentSet.equipmentSet.Set2option.GetAllOptionString()}";
        set4Text.text = $"4세트: {myEquipmentSet.equipmentSet.Set4option}";

        if (myEquipmentSet.activeCount >= 2)
        {
            set2Object.GetComponent<CanvasGroup>().alpha = 1;
            set2Text.color = new Color32(155, 255, 139, 255);
        }
        else
        {
            set2Text.color = Color.white;
            set2Object.GetComponent<CanvasGroup>().alpha = 0.5f;
        }

        if(myEquipmentSet.activeCount >= 4)
        {
            set4Object.GetComponent<CanvasGroup>().alpha = 1;
            set4Text.color = new Color32(155, 255, 139, 255);
        }
        else if(showAllSetEffect)
        {
            set4Object.GetComponent<CanvasGroup>().alpha = 0.5f;
            set4Text.color = Color.white;
        }
        else
        {
            set4Object.SetActive(false);
        }
    }
}
