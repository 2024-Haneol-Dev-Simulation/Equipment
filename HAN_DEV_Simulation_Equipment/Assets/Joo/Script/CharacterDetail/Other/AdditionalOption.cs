using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdditionalOption : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> optionTextList;
    public void Init(List<Option> options)
    {
        for(int i = 0; i < optionTextList.Count; i++)
        {
            if(i > options.Count-1)
            {
                optionTextList[i].gameObject.SetActive(false);
                continue;
            }

            optionTextList[i].gameObject.SetActive(true);
            optionTextList[i].text = "¡¤" + options[i].GetAllOptionString();
        }
    }
}
