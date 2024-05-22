using UnityEngine;

public class Gate : MonoBehaviour
{
    public int healthChange = -10;
    public int attackPowerChange = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log(other.gameObject);
        // �浹�� ������Ʈ�� Player �±׸� ������ �ִ��� Ȯ���մϴ�.
        if (other.gameObject.tag == "Player")
        {
            // Player ������Ʈ�� �����ɴϴ�.
            PlayerSample player = other.GetComponent<PlayerSample>();
            if (player != null)
            {
                // Player�� ü�°� ���ݷ��� �����մϴ�.
                player.health += healthChange;
                player.attackPower += attackPowerChange;

                // �α׸� ���� ����� ���� Ȯ���մϴ�.
                Debug.Log("Player health: " + player.health);
                Debug.Log("Player attackPower: " + player.attackPower);
            }
        }
    }
}
