using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float newSpeed = 3.0f;

    private void Start()
    {
        UpdateChildSpeeds();
    }

    private void Update()
    {
        UpdateChildSpeeds();
    }

    private void UpdateChildSpeeds()
    {
        foreach (Transform child in transform)
        {
            MapScrolling movingBackground = child.GetComponent<MapScrolling>();
            if (movingBackground != null)
            {
                movingBackground.speed = newSpeed;
            }
        }
    }

    public void SetNewSpeed(float newSpeedValue)
    {
        newSpeed = newSpeedValue;
    }
}
