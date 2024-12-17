using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoginController : MonoBehaviour
{
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Text messageText;

    private string apiUrl = "http://localhost:8000/"; // Đảm bảo URL đúng

    public void OnLoginButtonClicked()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        // Gọi API đăng nhập
        StartCoroutine(LoginUser(username, password));
    }

    private IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        string url = apiUrl + "/login.php"; // Gửi yêu cầu đăng nhập
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Kiểm tra phản hồi từ API
            string response = www.downloadHandler.text;
            if (response.Contains("Đăng nhập thành công"))
            {
                messageText.text = "Đăng nhập thành công!";
                SceneManager.LoadScene("Map3");
            }
            else
            {
                messageText.text = "Sai tên đăng nhập hoặc mật khẩu.";
            }
        }
        else
        {
            messageText.text = "Lỗi kết nối: " + www.error;
        }
    }
}
