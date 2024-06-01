using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementAdditionalOption : MonoBehaviour
{
    [SerializeField] private GameObject Notice;

    [SerializeField] private List<TMP_Text> AdditionalOptionTypeTextList;
    [SerializeField] private List<TMP_Text> AdditionalOptionValueTextList;

    [SerializeField] private List<GameObject> AdditionalOptionElementList;
    [SerializeField] private List<Image> AdditionalOptionEffectList;
    public delegate void EventChangeAdditionalOption(int index);
    public void Init(List<Option> options)
    {
        Notice.SetActive(false);

        for(int i =0;i < 4;i++)
        {
            if (options.Count - 1 < i)
            {
                AdditionalOptionElementList[i].SetActive(false);
                continue;
            }

            AdditionalOptionElementList[i].SetActive(true);

            AdditionalOptionTypeTextList[i].text = "¡¤ " + options[i].GetOptionTypeString();
            AdditionalOptionValueTextList[i].text = options[i].GetOptionValueString();
        }
    }

    public void ChangeAdditionalOption(int index)
    {
        AdditionalOptionEffectList[index].DOKill();
        AdditionalOptionEffectList[index].DOFade(1, 0.2f).OnComplete(() =>
        {
            AdditionalOptionEffectList[index].DOFade(0, 0.2f);
        });
    }
}
