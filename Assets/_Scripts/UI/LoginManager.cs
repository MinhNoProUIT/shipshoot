using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro; 
using System.Threading.Tasks;


public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;  
    [SerializeField] private TMP_InputField password;   
    [SerializeField] private Button login;              

    private FirebaseAuth auth;

    void Start()
    {
        InitializeFirebase();

        login.onClick.AddListener(OnLoginButtonClicked);
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase initialized successfully.");

                if (string.IsNullOrEmpty(app.Options.DatabaseUrl.ToString())) 
                {
                    app.Options.DatabaseUrl = new System.Uri("https://shipshooting-default-rtdb.firebaseio.com/"); // Thay thế bằng URL thực tế của bạn
                    Debug.LogWarning("Database URL has been set: " + app.Options.DatabaseUrl);
                }
                else
                {
                    Debug.LogWarning("Database URL is already set: " + app.Options.DatabaseUrl);
                }

                auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase: " + task.Exception);
            }
        });
    }

    private void OnLoginButtonClicked()
    {
        string email = username.text;
        string pass = password.text;

        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                foreach (var exception in task.Exception.Flatten().InnerExceptions)
                {
                    FirebaseException firebaseEx = exception as FirebaseException;
                    Debug.LogWarning($"Firebase Error Code: {firebaseEx.ErrorCode}, Message: {firebaseEx.Message}");
                }
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            
        });
    }
}
