using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundImage : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private bool AddListener;
    [SerializeField] private Color newColoer;
    private void Start()
    {
        if (AddListener)
            GetComponent<Button>().onClick.AddListener(() => { ChangeImage(newColoer); });
    }
    public void ChangeImage(Color color)
    {

        background.DOKill();
        background.DOColor(color,0.3f);
    }
}
