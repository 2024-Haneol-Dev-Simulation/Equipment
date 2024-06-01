using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(moveX,0f, 0f) * moveSpeed * Time.deltaTime;

        transform.position += move;
    }
}
