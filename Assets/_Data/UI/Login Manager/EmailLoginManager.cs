using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Threading;

public class EmailLoginManager : BaseAuthManager
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI forgotPasswordText;
    [SerializeField] private TextMeshProUGUI registerText;
    [SerializeField] protected Transform registerManager;
    private SynchronizationContext mainThreadContext;

    protected override void Start()
    {
        base.Start();
        loginButton.onClick.AddListener(SignIn);
        mainThreadContext = SynchronizationContext.Current;

        
        // Thêm EventTrigger cho forgotPasswordText
        EventTrigger forgotPasswordTrigger = forgotPasswordText.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry forgotEntry = new EventTrigger.Entry();
        forgotEntry.eventID = EventTriggerType.PointerClick;
        forgotEntry.callback.AddListener((data) => { OnForgotPasswordClicked(); });
        forgotPasswordTrigger.triggers.Add(forgotEntry);

        // Thêm EventTrigger cho registerText
        EventTrigger registerTrigger = registerText.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry registerEntry = new EventTrigger.Entry();
        registerEntry.eventID = EventTriggerType.PointerClick;
        registerEntry.callback.AddListener((data) => { OnRegisterClicked(); });
        registerTrigger.triggers.Add(registerEntry);
    }

    public override  void SignIn()
    {
        if (auth == null)
        {
            Debug.LogError("Auth is not initialized.");
            return;
        }

        if (emailInput == null || passwordInput == null)
        {
            Debug.LogError("Email or Password input is not assigned.");
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                switch (errorCode)
                {
                    case AuthError.WrongPassword:
                        Debug.LogWarning("Sai mật khẩu!");
                        break;
                    case AuthError.UserNotFound:
                        Debug.LogWarning("Tài khoản không tồn tại!");
                        break;
                    default:
                        Debug.LogWarning($"Đăng nhập thất bại: {firebaseEx.Message}");
                        ClearInputFields();
                        break;
                }
                return;
            }
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Đăng nhập thành công!");
                mainThreadContext.Post(_ =>
                {
                    SceneManager.LoadScene("Shopping");
                }, null);
            }

            FirebaseUser user = task.Result.User; 
            SaveUserData(user.UserId);

            Debug.Log($"Đăng nhập thành công với email: {user.Email}");
            //Debug.Log("Load scene Shopping");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Shopping");
            //LevelManager.instance.LoadScene("Shopping");
            Debug.Log("Bắt đầu load scene...");
            SceneManager.LoadScene("Shopping");
            Debug.Log("Scene đã được load (nếu dòng này không xuất hiện, kiểm tra Build Settings).");


        });
    }

    private void OnForgotPasswordClicked()
    {
        string email = emailInput.text;
        if (string.IsNullOrEmpty(email))
        {
            Debug.LogWarning("Vui lòng nhập email!");
            return;
        }

        auth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Gửi email khôi phục thất bại: " + task.Exception);
                return;
            }
            Debug.Log("Đã gửi email khôi phục mật khẩu!");
        });
    }

    private void OnRegisterClicked()
    {
        // TODO: Chuyển sang màn hình đăng ký
        // Ví dụ: SceneManager.LoadScene("RegisterScene");
        Debug.Log("Chuyển sang màn hình đăng ký");
        registerManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearInputFields()
    {
        emailInput.text = string.Empty;
        passwordInput.text = string.Empty;
    }

    public override void SignOut()
    {
        auth.SignOut();
        Debug.Log("Đã đăng xuất");
    }

    protected virtual void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
