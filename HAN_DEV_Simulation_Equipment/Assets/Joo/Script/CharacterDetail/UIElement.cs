using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : MonoBehaviour
{
    public delegate void Complete();
    public abstract void Show(ShowType showType, Complete complete = null);
    public abstract void Hide(HideType hideType, Complete complete = null);
}

public enum ShowType
{
    None,
    Fade,
    FadeAndMove
}
public enum HideType
{
    None,
    Fade,
    FadeAndMove
}
