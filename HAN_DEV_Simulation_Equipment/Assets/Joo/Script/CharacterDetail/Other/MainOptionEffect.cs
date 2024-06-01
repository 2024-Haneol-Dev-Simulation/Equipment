using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainOptionEffect : MonoBehaviour
{
    [SerializeField] private GameObject TempObj;
    [SerializeField] private TMP_Text tempText;
    [SerializeField] private TMP_Text MainOptionValueText;


    public void OnEffect(string orjValue, string nextValue)
    {
        TempObj.SetActive(true);
        tempText.text = orjValue;
        MainOptionValueText.text = nextValue;

        MainOptionValueText.color = new Color32(214, 167, 100,255);
        MainOptionValueText.rectTransform.anchoredPosition = new Vector2(-50.9f, 0);
    }
    public void ImmediatelyOffEffect()
    {
        TempObj.SetActive(false);
        MainOptionValueText.color = Color.white;
        MainOptionValueText.rectTransform.anchoredPosition = new Vector2(-41.9f, 0);
    }
    public void OffEffect(string str)
    {
        TempObj.SetActive(false);
        MainOptionValueText.color = Color.white;

        if (str != string.Empty)
            MainOptionValueText.text = str;

        MainOptionValueText.rectTransform.DOAnchorPos(new Vector2(-41.9f, 0),0.3f);
    }
}
