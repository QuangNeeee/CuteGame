using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ApiController : MonoBehaviour
{
    private string apiUrl = "http://localhost:8000/api.php"; // URL của API

    // Đăng ký người dùng
    public void RegisterUser(string username, string email, string password)
    {
        StartCoroutine(PostRequest("register", username, email, password));
    }

    // Đăng nhập người dùng
    public void LoginUser(string username, string password)
    {
        StartCoroutine(PostRequest("login", username, password));
    }

    // Gửi yêu cầu POST đến API PHP
    private IEnumerator PostRequest(string action, string username, string email = "", string password = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        if (action == "register")
        {
            form.AddField("email", email); // Chỉ cần gửi email khi đăng ký
        }

        // URL API kèm theo action
        string url = apiUrl + "?action=" + action;

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Response: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }
}
