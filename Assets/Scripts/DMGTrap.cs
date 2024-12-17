using UnityEngine;
using System.Collections;


public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;  // Lượng sát thương gây ra
    [SerializeField] private bool canDamage = true; // Trạng thái có thể gây sát thương
    [SerializeField] private float damageCooldown = 1f; // Thời gian hồi giữa các lần sát thương

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Player không
        if (collision.CompareTag("Player") && canDamage)
        {
            // Gọi hàm TakeDamage từ PlayerHealth
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount, transform);
                StartCoroutine(DamageCooldownRoutine());
            }
        }
    }

    private IEnumerator DamageCooldownRoutine()
    {
        canDamage = false; // Tạm thời không gây sát thương
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true; // Cho phép gây sát thương lại
    }
}
