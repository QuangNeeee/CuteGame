using UnityEngine;

public class SlowDownMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Tốc độ di chuyển ban đầu
    public float slowDownRate = 0.95f; // Hệ số giảm tốc (0.95 = giảm 5% mỗi frame)

    void Update()
    {
        // Di chuyển đối tượng về phía trước
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Giảm tốc độ di chuyển dần dần
        moveSpeed *= slowDownRate;

        // Giới hạn tốc độ không âm
        if (moveSpeed < 0.1f)
        {
            moveSpeed = 0f;
        }
    }
}
