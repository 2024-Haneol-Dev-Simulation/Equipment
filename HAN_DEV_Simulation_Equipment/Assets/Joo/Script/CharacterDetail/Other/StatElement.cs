using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatElement : MonoBehaviour
{
    public void Init(string Type, string value)
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = Type;
        transform.GetChild(2).GetComponent<TMP_Text>().text = value;
    }
}
