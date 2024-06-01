using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class MenuListListener : UIElement
{
    public List<Button> menuElementList;
    public List<UIEventList> menuEventActions;
    private void Start()
    {
        for (int i = 0; i < menuElementList.Count; i++)
        {
            for (int j = 0; j < menuEventActions[i].menuEvents.Count; j++)
            {
                UIElement.Complete action = null;
                if (menuEventActions[i].menuEvents[j].CompleteList != null ||
                    menuEventActions[i].menuEvents[j].CompleteList.Count != 0)
                {
                    for(int k = 0; k < menuEventActions[i].menuEvents[j].CompleteList.Count; k++)
                    {
                        int n1 = i,n2 = j,n3 = k;
                        action += () =>
                        {
                            menuEventActions[n1].menuEvents[n2].CompleteList[n3].GetAction()();
                        };
                    }
                }
                menuElementList[i].onClick.AddListener(menuEventActions[i].menuEvents[j].GetAction(action));
            }
        }
    }
    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        gameObject.SetActive(true);
        if (showType == ShowType.FadeAndMove)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 55.45f, 0);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(new Vector3(-14, 55.45f, 0), 0.3f);
        }
        else if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }
}
[Serializable]
public class UIEventList
{
    public List<UIEvent> menuEvents;
}

[Serializable]
public class UIEvent
{
    public UIElement element;
    public ShowType showType;
    public HideType hideType;
    public List<UIEvent> CompleteList;
    public UnityAction GetAction(UIElement.Complete complete = null)
    {
        UnityAction unityAction = () =>
        {
            if(showType != ShowType.None)
            {
                element.Show(showType, complete);
            }
            else if(hideType != HideType.None)
            {
                element.Hide(hideType, complete);
            }
        };

        return unityAction;
    }
}