using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    
    [SerializeField] private UIEventList eventList;
    
    void Start()
    {
        for (int j = 0; j < eventList.menuEvents.Count; j++)
        {
            UIElement.Complete action = null;
            if (eventList.menuEvents[j].CompleteList != null ||
                eventList.menuEvents[j].CompleteList.Count != 0)
            {
                for (int k = 0; k < eventList.menuEvents[j].CompleteList.Count; k++)
                {
                    int n2 = j, n3 = k;
                    action += () =>
                    {
                        eventList.menuEvents[n2].CompleteList[n3].GetAction()();
                    };
                }
            }
            gameObject.GetComponent<Button>().onClick.AddListener(eventList.menuEvents[j].GetAction(action));

            
        }
        
    }
}
