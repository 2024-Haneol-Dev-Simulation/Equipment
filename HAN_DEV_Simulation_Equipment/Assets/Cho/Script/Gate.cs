using UnityEngine;

public class Gate : MonoBehaviour
{
    public int healthChange = -10;
    public int attackPowerChange = 5;

    public float speed = 5.0f; // 오브젝트의 낙하 속도
    public float resetHeight = -10.0f; // 이 높이 이하로 내려갔을 때 리셋
    public float startPositionY = 10.0f; // 리셋 위치의 Y 좌표

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < resetHeight)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            PlayerSample player = other.GetComponent<PlayerSample>();
            if (player != null)
            {

                player.health += healthChange;
                player.attackPower += attackPowerChange;

                Debug.Log("Player health: " + player.health);
                Debug.Log("Player attackPower: " + player.attackPower);
            }
        }
    }
}
