using UnityEngine;

public class HealthSpriteChanger : MonoBehaviour
{
    // Biến máu hiện tại và ngưỡng máu để đổi hình
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int thresholdHealth = 50;
    private int currentHealth;

    // SpriteRenderer và các sprite thay đổi
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite normalSprite;      // Hình ảnh bình thường
    [SerializeField] private Sprite damagedSprite;     // Hình ảnh khi máu xuống thấp

    void Start()
    {
        // Lấy SpriteRenderer từ đối tượng
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Khởi tạo máu
        currentHealth = maxHealth;
        spriteRenderer.sprite = normalSprite;
    }

    // Gọi hàm này khi đối tượng bị giảm máu
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= thresholdHealth)
        {
            ChangeSpriteToDamaged();
        }
    }

    private void ChangeSpriteToDamaged()
    {
        // Đổi hình ảnh khi máu giảm xuống ngưỡng
        spriteRenderer.sprite = damagedSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            // Giảm 20 máu khi va chạm với đối tượng Enemy
            GetComponent<HealthSpriteChanger>().TakeDamage(20);
        }
    }

}
