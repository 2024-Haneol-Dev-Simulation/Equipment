using UnityEngine;

public class Gate : MonoBehaviour
{
    public int healthChange = -10;
    public int attackPowerChange = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log(other.gameObject);
        // 충돌한 오브젝트가 Player 태그를 가지고 있는지 확인합니다.
        if (other.gameObject.tag == "Player")
        {
            // Player 컴포넌트를 가져옵니다.
            PlayerSample player = other.GetComponent<PlayerSample>();
            if (player != null)
            {
                // Player의 체력과 공격력을 변경합니다.
                player.health += healthChange;
                player.attackPower += attackPowerChange;

                // 로그를 통해 변경된 값을 확인합니다.
                Debug.Log("Player health: " + player.health);
                Debug.Log("Player attackPower: " + player.attackPower);
            }
        }
    }
}
