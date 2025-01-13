using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public abstract class BaseAuthManager : MonoBehaviour
{
    protected FirebaseAuth auth;
    protected DatabaseReference dbReference;

    protected virtual void Start()
    {
        InitializeFirebase();
    }

    protected void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.IsCompleted && !task.IsFaulted)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;
                if(auth == null) {
                    Debug.Log("Auth null");
                }
                else {
                    Debug.Log("Auth not null");
                }
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                OnFirebaseInitialized();
            }
            else
            {
                Debug.LogError("Firebase initialization failed");
            }
        });
    }

    protected virtual void OnFirebaseInitialized() { }

    protected void SaveUserData(string userId)
    {
        DatabaseReference userRef = dbReference.Child("users").Child(userId);

        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { "email", auth.CurrentUser.Email },
            { "displayName", auth.CurrentUser.DisplayName },
            { "lastLogin", DateTime.UtcNow.ToString("o") }
        };

        userRef.UpdateChildrenAsync(userData);
    }

    public abstract void SignIn();
    public abstract void SignOut();
}
