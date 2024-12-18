using System.Collections;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;                // Máu ban đầu
    [SerializeField] private GameObject deathVFXPrefab;            // Hiệu ứng chết
    [SerializeField] private float knockBackThrust = 15f;          // Lực đẩy khi bị đánh
    [SerializeField] private float chaseSpeed = 3f;                // Tốc độ bay về phía Player
    [SerializeField] private float damageInterval = 1f;            // Khoảng thời gian gây sát thương
    [SerializeField] private int damagePerSecond = 1;              // Sát thương mỗi giây khi tiếp cận Player
    [SerializeField] private int lowHealthThreshold = 1;           // Ngưỡng máu thấp để bắt đầu bay vào Player

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;
    private Transform playerTransform;                             // Vị trí của Player
    private bool isChasing = false;                                // Kẻ địch đang đuổi theo Player
    private bool isTouchingPlayer = false;                         // Kẻ địch đang tiếp xúc với Player

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;

        // Tìm Player trong scene (theo tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // Nếu máu thấp hơn ngưỡng và chưa bắt đầu bay, bắt đầu đuổi theo Player
        if (currentHealth <= lowHealthThreshold && playerTransform != null)
        {
            isChasing = true;
        }

        // Nếu đang đuổi theo Player, di chuyển về phía Player
        if (isChasing)
        {
            FlyTowardPlayer();
        }
    }

    private void FlyTowardPlayer()
    {
        // Di chuyển về phía Player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            StartCoroutine(DamagePlayerOverTime(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator DamagePlayerOverTime(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        while (isTouchingPlayer)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond, transform);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            Destroy(gameObject);
        }
    }
}
