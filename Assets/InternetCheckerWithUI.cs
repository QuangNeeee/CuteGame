using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InternetCheckerWithUI : MonoBehaviour
{
    public Text statusText; // Thêm Text UI để hiển thị trạng thái mạng

    private string testUrl = "https://google.com";

    void Start()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            if (isConnected)
            {
                statusText.text = "Đã kết nối mạng";
                Debug.Log("Unity có kết nối mạng!");
            }
            else
            {
                statusText.text = "Không có kết nối mạng";
                Debug.LogWarning("Unity không có kết nối mạng.");
            }
        }));
    }

    private System.Collections.IEnumerator CheckInternetConnection(System.Action<bool> action)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            request.timeout = 5;
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                action(true);
            }
            else
            {
                action(false);
            }
        }
    }
}
