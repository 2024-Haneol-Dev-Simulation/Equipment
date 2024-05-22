using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTransitionListener : MonoBehaviour
{
    [SerializeField] private CameraTransition cameraTransition;
    [SerializeField] private CamNumder camNumder;
    void Start()
    {
        if (camNumder != CamNumder.Default)
            gameObject.GetComponent<Button>().onClick.AddListener(() => { cameraTransition.ChangeCamera(camNumder); });
    }
}
