using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitEquimentListUI : MonoBehaviour
{
    [SerializeField] private EquipmentListMenu equipmentListMenu;
    [SerializeField] private EquipmentSystem equipmentSystem;
    [SerializeField] private CharacterEquimentButton characterEquimentButton;
    [SerializeField] private int OpenIndex;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            characterEquimentButton.AllHideButton(OpenIndex);
            equipmentSystem.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Selct());
        });
    }
    private IEnumerator Selct()
    {
        yield return new WaitForEndOfFrame();

        equipmentListMenu.ImmediatelyOpenEquipmentList(OpenIndex);

    }
}
