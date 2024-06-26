using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnhancementTop : UIElement
{
    public override void Hide(HideType hideType, Complete complete = null)
    {
        gameObject.SetActive(false);
    }

    public override void Show(ShowType showType, Complete complete = null)
    {
        gameObject.SetActive(true);
        if (showType == ShowType.FadeAndMove)
        {
            Vector3 pos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(pos.x, 0, pos.z);
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(pos, 0.3f);
        }
        else if (showType == ShowType.Fade)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        }
    }
}
