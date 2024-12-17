using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoginManager : MonoBehaviour
{
    // Tham chiếu đến các thành phần UI
    public InputField usernameInput;       // Ô nhập Username
    public InputField passwordInput;       // Ô nhập Password
    public TextMeshProUGUI notification;   // Text hiển thị thông báo
    public Button loginButton;             // Nút đăng nhập

    // Tài khoản và mật khẩu mẫu
    private string correctUsername = "DungHoang";
    private string correctPassword = "1812";

    private void Start()
    {
        // Gắn sự kiện cho nút đăng nhập
        loginButton.onClick.AddListener(LoginButton);
        // Ẩn mật khẩu
        passwordInput.contentType = InputField.ContentType.Password;
        passwordInput.ActivateInputField();
    }

    // Hàm xử lý khi nhấn nút đăng nhập
    void LoginButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Kiểm tra dữ liệu rỗng trước
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            notification.text = "Khong duoc bo trong du lieu!!";
            notification.color = Color.red;
            Debug.Log("DU lieu bi bo trong!!");

            // Reset về giá trị rỗng
            usernameInput.text = "";
            passwordInput.text = "";
        }
        // Kiểm tra tài khoản và mật khẩu
        else if (username == correctUsername && password == correctPassword)
        {
            notification.text = "Dang nhap thanh cong!!";
            notification.color = Color.green;
            Debug.Log("Dang nhap thanh cong!!");

            // Chuyển sang Scene khác sau khi đăng nhập thành công
            SceneManager.LoadScene("Map3"); 
        }
        // Sai tài khoản hoặc mật khẩu
        else
        {
            notification.text = "Du lieu khong trung khop";
            notification.color = Color.red;
            Debug.Log("Dang nhap that bai!!");

            // Reset về giá trị rỗng
            usernameInput.text = "";
            passwordInput.text = "";
        }
    }
}
