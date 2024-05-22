using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterEquimentButton : UIElement
{
    [SerializeField] private List<Button> buttons;
    
    public void AllHideButton(int index = -1)
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            if(index != i)
            {
                buttons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                
            }
            buttons[i].GetComponent<Image>().raycastTarget = false;
            //buttons[i].interactable = false;
        }
    }
    public void AllShowButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //buttons[i].interactable = true;
            buttons[i].GetComponent<Image>().raycastTarget = true;
        }
    }
    public void HideButton(int index)
    {
        buttons[index].GetComponent<Image>().DOFade(0, 0.3f);
    }
    public void ShowButton(int index)
    {
        buttons[index].GetComponent<Image>().DOFade(1, 0.3f);
    }

    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        AllShowButton();
        gameObject.SetActive(true);
        if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }


}
