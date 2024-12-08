using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;

public class EmailAuthManager : BaseAuthManager
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;

    protected override void Start()
    {
        base.Start();
        loginButton.onClick.AddListener(SignIn);
        registerButton.onClick.AddListener(Register);
    }

    protected override void SignIn()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Login failed: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;
            SaveUserData(user.UserId);
        });
    }

    private void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (password != confirmPassword)
        {
            Debug.LogWarning("Passwords do not match!");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                switch (errorCode)
                {
                    case AuthError.EmailAlreadyInUse:
                        Debug.LogWarning("Email đã được sử dụng!");
                        break;
                    case AuthError.WeakPassword:
                        Debug.LogWarning("Mật khẩu quá yếu!");
                        break;
                    case AuthError.InvalidEmail:
                        Debug.LogWarning("Email không hợp lệ!");
                        break;
                    default:
                        Debug.LogError($"Đăng ký thất bại: {firebaseEx.Message}");
                        break;
                }
                return;
            }

            FirebaseUser newUser = task.Result.User;
            SaveUserData(newUser.UserId);
            Debug.Log($"Đăng ký thành công với email: {newUser.Email}");
            
            ClearInputFields();
        });
    }

    private void ClearInputFields()
    {
        emailInput.text = string.Empty;
        passwordInput.text = string.Empty;
        confirmPasswordInput.text = string.Empty;
    }

    protected override void SignOut()
    {
        auth.SignOut();
    }
}
