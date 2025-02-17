using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase;
using UnityEngine.EventSystems;
using System;
using System.Threading;
using DG.Tweening;

public class EmailRegisterManager : BaseAuthManager
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private TextMeshProUGUI backToLoginText;
    [SerializeField] protected Transform loginManager;

    [SerializeField] protected Transform registerSuccessful;
    [SerializeField] protected Transform registerFailed;

    private SynchronizationContext mainThreadContext;

    protected override void OnEnable()
    {
        base.OnEnable();
        AppearanceEffect();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CloseEffect();
    }

    protected virtual void AppearanceEffect(){
        transform.localScale = Vector3.zero; // Đặt scale ban đầu là 0,0,0
        transform.DOScale(new Vector3(6,9,1), 0.5f).SetEase(Ease.OutBack);
    }

    protected virtual void CloseEffect()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack); // Scale xuống 0,0,0 trong 0.5s
    }


    protected override void Start()
    {
        base.Start();
        mainThreadContext = SynchronizationContext.Current;

        registerButton.onClick.AddListener(Register);
        //backToLoginText.onClick.AddListener(OnBackToLoginClicked);

         EventTrigger backToLoginTrigger = backToLoginText.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry backToLoginEntry = new EventTrigger.Entry();
        backToLoginEntry.eventID = EventTriggerType.PointerClick;
        backToLoginEntry.callback.AddListener((data) => { OnBackToLoginClicked(); });
        backToLoginTrigger.triggers.Add(backToLoginEntry);

    }

    private void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogWarning("Vui lòng điền đầy đủ thông tin!");
            return;
        }

        if (password != confirmPassword)
        {
            Debug.LogWarning("Mật khẩu xác nhận không khớp!");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                FirebaseException firebaseEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                mainThreadContext.Post(_ =>
                {
                    registerFailed.gameObject.SetActive(true);
                    gameObject.SetActive(false);

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
                }, null);
                return;
            }

            mainThreadContext.Post(_ =>
            {
                try
                {
                    FirebaseUser newUser = task.Result.User;
                    Debug.Log($"Đăng ký thành công với email: {newUser.Email}");

                    ClearInputFields();
                    Debug.Log("Đã xóa các trường nhập liệu");

                    // Kiểm tra xem phương thức có được gọi không
                    Debug.Log("Gọi OnBackToLoginClicked");
                    registerSuccessful.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"An error occurred: {ex.Message}");
                    registerFailed.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                }
            }, null);
        });
    }

    private void OnBackToLoginClicked()
    {
        // TODO: Chuyển về màn hình đăng nhập
        // Ví dụ: SceneManager.LoadScene("LoginScene");
        Debug.Log("Chuyển về màn hình đăng nhập");
        loginManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ClearInputFields()
    {
        emailInput.text = string.Empty;
        passwordInput.text = string.Empty;
        confirmPasswordInput.text = string.Empty;
    }

    public override void SignIn()
    {
        // Không cần implement vì class này chỉ để đăng ký
    }

    public override void SignOut()
    {
        // Không cần implement vì class này chỉ để đăng ký
    }
}
