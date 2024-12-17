using UnityEngine;
using System.Collections;


public class SlimeExplode : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float detectionRadius = 3f;      // Bán kính phát hiện Player
    [SerializeField] private int explosionDamage = 1;         // Lượng sát thương gây ra khi nổ
    [SerializeField] private GameObject explosionEffect;      // Hiệu ứng nổ (Prefab Particle System)

    [Header("Slime Settings")]
    [SerializeField] private float explosionDelay = 0.5f;     // Thời gian trễ trước khi nổ

    private Transform player;       // Vị trí của Player
    private bool hasExploded = false; // Tránh nổ nhiều lần

    private void Start()
    {
        // Tìm Player trong Scene dựa trên Tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Kiểm tra khoảng cách giữa Slime và Player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !hasExploded)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        hasExploded = true;

        // Thêm hiệu ứng trễ trước khi nổ
        yield return new WaitForSeconds(explosionDelay);

        // Hiển thị hiệu ứng nổ
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Gây sát thương cho Player
        if (player.GetComponent<PlayerHealth>() != null)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(explosionDamage, transform);
        }

        // Hủy đối tượng Slime sau khi nổ
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị bán kính phát hiện trong Scene
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
