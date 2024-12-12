using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    public Transform respawmPoint;

    SpriteRenderer spriteRenderer;
    public Sprite passive, active;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.UpdateCheckPoint(transform.position);
            spriteRenderer.sprite = active;
        }
    }
}
