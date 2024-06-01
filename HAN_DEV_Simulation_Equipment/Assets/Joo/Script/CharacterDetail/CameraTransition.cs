using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    private CamNumder nowCamera;
    private CinemachineClearShot clearShot;
    void Start()
    {
        nowCamera = CamNumder.AllCamera;
        clearShot = GetComponent<CinemachineClearShot>();
        
    }

    public void ChangeCamera(CamNumder camera)
    {
        clearShot.ChildCameras[(int)nowCamera-1].Priority = 10;
        clearShot.ChildCameras[(int)camera-1].Priority = 11;

        nowCamera = camera;
    }
}

public enum CamNumder
{
    Default,
    AllCamera,
    DetailCamera,
    HeadCamera,
    WingCamera,
    MissleCamera,
    EngineCamera
}
