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
using System;
using Firebase.Database;
using DG.Tweening;

public class EmailLoginManager : BaseAuthManager
{
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI forgotPasswordText;
    [SerializeField] private TextMeshProUGUI registerText;
    [SerializeField] protected Transform registerManager;
    [SerializeField] protected Transform resetPasswordManager;
    [SerializeField] protected Transform loginSuccessful;
    [SerializeField] protected Transform loginFailed;
    //[SerializeField] GameObject firebaseManager;
    private SynchronizationContext mainThreadContext;
    //[SerializeField] GameObject databaseManager;

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
        //firebaseManager.SetActive(false);
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
            Debug.Log("Auth is not initialized.");
            return;
        }

        if (emailInput == null || passwordInput == null)
        {
            Debug.Log("Email or Password input is not assigned.");
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsFaulted)
            {
                

                FirebaseException firebaseEx = task.Exception?.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                mainThreadContext.Post(_=>{
                    loginFailed.gameObject.SetActive(true);
                        gameObject.SetActive(false);
                },null);

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
               // databaseManager.SetActive(true);
                Debug.Log("Đăng nhập thành công!");
                FirebaseUser user = task.Result.User;
                Debug.Log($"Đăng nhập thành công với email: {user.Email}");

                string userId = user.UserId;
                string userEmail = user.Email;
                //PlayerPrefs.SetString("UserId", userId);
                //PlayerPrefs.Save();

                //firebaseManager.SetActive(true);

                //DatabaseManager.Instance.SetUserId(userId);

                // Kiểm tra xem dữ liệu người dùng đã tồn tại chưa
                CheckIfUserDataExists(userId, exists =>
                {
                    if (exists)
                    {
                        // Nếu dữ liệu đã tồn tại, load dữ liệu hiện tại
                        Debug.Log("Dữ liệu đã tồn tại. Đang tải dữ liệu...");
                        Initialize(userId); // Load dữ liệu hiện có
                        //SceneManager.LoadScene("MainScene");
                    }
                    else
                    {
                        // Nếu dữ liệu chưa tồn tại, khởi tạo dữ liệu mặc định
                        Debug.Log("Dữ liệu không tồn tại. Đang thiết lập dữ liệu mặc định...");
                        InitializeDefaultData(userId, userEmail, success =>
                        {
                            if (success)
                            {
                                Debug.Log("Thiết lập dữ liệu mặc định thành công.");
                                //SceneManager.LoadScene("MainScene");
                            }
                            else
                            {
                                Debug.LogError("Không thể thiết lập dữ liệu mặc định.");
                            }
                        });
                    }
                    
                });

                mainThreadContext.Post(_ =>
                {
                    PlayerPrefs.SetString("UserId", userId);
                    PlayerPrefs.Save();

                    loginSuccessful.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    //PlayfabManager.Instance.LoginToPlayFab(user.UserId);
                    //SceneManager.LoadScene("MainScene");
                }, null);
                //PlayfabManager.Instance.LoginToPlayFab(task.Result.User.UserId);
                //mainThreadContext.Post(_ =>
                //{
                //   SceneManager.LoadScene("Shopping");
                //}, null);
            }

            /* FirebaseUser user = task.Result.User; 
            SaveUserData(user.UserId);

            Debug.Log($"Đăng nhập thành công với email: {user.Email}");
            //Debug.Log("Load scene Shopping");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Shopping");
            //LevelManager.instance.LoadScene("Shopping");
            Debug.Log("Bắt đầu load scene...");
            mainThreadContext.Post(_ =>
            {
                PlayfabManager.Instance.LoginToPlayFab(user.UserId);
                SceneManager.LoadScene("MainScene");
            }, null); */
            //SceneManager.LoadScene("Shopping");
            //Debug.Log("Scene đã được load (nếu dòng này không xuất hiện, kiểm tra Build Settings).");


        });
    }



    public void CheckIfUserDataExists(string userId, Action<bool> callback)
    {
        DatabaseReference userRef = dbReference.Child("users").Child(userId);

        userRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi kiểm tra dữ liệu người dùng: {task.Exception}");
                callback?.Invoke(false); // Trả về false nếu có lỗi
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                callback?.Invoke(snapshot.Exists); // Chỉ kiểm tra dữ liệu của userId
            }
        });
    }


    public void Initialize(string userId)
    {
        Debug.LogWarning("Initialize" + userId);
        LoadResources(userId);
    }

    private void LoadResources(string userId)
    {
        dbReference.Child("users").Child(userId).Child("resources").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi tải tài nguyên: {task.Exception}");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                //if (snapshot.Exists)
                //{
                //    Golds.Value = int.Parse(snapshot.Child("gold").Value.ToString());
                //    Diamonds.Value = int.Parse(snapshot.Child("diamonds").Value.ToString());
                //}
            }
        });
    }

    public void InitializeDefaultData(string userId, string email, Action<bool> callback)
    {
        var defaultData = new Dictionary<string, object>
        {
            { "email", email },
            { "lastLogin", DateTime.UtcNow.ToString("o") },
            { "resources", new Dictionary<string, object>
                {
                    { "gold", 1000 },
                    { "diamonds", 100 }
                }
            },
            { "ownedSpaceships", new Dictionary<string, int>
                {
                    {"SH001", 1}
                }
            },
            { "settings", new Dictionary<string, object>
                {
                    { "backgroundMusic", true },
                    { "soundEffects", true },
                    { "bulletSoundVolume", 50 }
                }
            },
            { "gameStats", new Dictionary<string, int>() }
        };

        dbReference.Child("users").Child(userId).UpdateChildrenAsync(defaultData).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi thiết lập dữ liệu mặc định: {task.Exception}");
                callback?.Invoke(false);
            }
            else
            {
                Debug.Log("Thiết lập dữ liệu mặc định thành công.");
                callback?.Invoke(true);
            }
        });
    }

    private void OnForgotPasswordClicked()
    {
        Debug.Log("Chuyển sang màn hình thay đổi mật khẩu");
        resetPasswordManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
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
