using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine;
using System.Threading;

public class DatabaseManager : BaseMonoBehaviour
{
    private static DatabaseManager instance;
    public static DatabaseManager Instance => instance;
    //public static DatabaseManager Instance { get; private set; }
    private DatabaseReference dbReference;
    public ObservableValue<int> Golds { get; private set; }
    public ObservableValue<int> Diamonds { get; private set; }
    private string userId;
    SynchronizationContext unityContext;
    

    public void SetUserId(string id)
    {
        userId = id;
    }

    public string GetUserId()
    {
        return PlayerPrefs.GetString("UserId", "");
    }


    protected override void Awake()
    {
        base.Awake();
        if (DatabaseManager.instance != null) Debug.LogError("Only 1 PlayfabManager allow to exist");
        DatabaseManager.instance = this;

        userId = PlayerPrefs.GetString("UserId", "");

        unityContext = SynchronizationContext.Current;

        DontDestroyOnLoad(gameObject);
        
    }

    protected override void Start()
    {
        base.Start();
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        if (!string.IsNullOrEmpty(userId))
        {
            Debug.Log($"UserId từ PlayerPrefs: {userId}");
            //LoadUserData(userId);
        }
        else
        {
            Debug.Log($"UserId NULL");

        }
        Golds = new ObservableValue<int>(100);
        Diamonds = new ObservableValue<int>(100);

        Golds.OnValueChanged += value => SaveResource("gold", value);
        Diamonds.OnValueChanged += value => SaveResource("diamonds", value);
    }

    private void LoadUserData(string userId, Action onComplete)
    {
        dbReference.Child("users").Child(userId).Child("resources").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Debug.Log("Tải dữ liệu người dùng thành công!");

                    // Lấy giá trị từ database
                    int golds = snapshot.Child("gold").Exists ? int.Parse(snapshot.Child("gold").Value.ToString()) : 0;
                    int diamonds = snapshot.Child("diamonds").Exists ? int.Parse(snapshot.Child("diamonds").Value.ToString()) : 0;

                    Debug.Log($"Golds: {golds}, Diamonds: {diamonds}");

                    // Lưu vào PlayerPrefs
                    PlayerPrefs.SetInt("Golds", golds);
                    PlayerPrefs.SetInt("Diamonds", diamonds);
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy dữ liệu người dùng.");
                }
            }
            else
            {
                Debug.LogError($"Lỗi khi tải dữ liệu từ Firebase: {task.Exception}");
            }

            // Gọi callback khi hoàn tất
            onComplete?.Invoke();
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
                callback?.Invoke(snapshot.Exists); // Trả về true nếu dữ liệu tồn tại, ngược lại false
            }
        });
    }

    public void Initialize(string userId)
    {
        this.userId = userId;
        LoadResources();
    }

    public void LoadResources(string userId, Action<bool> callback = null)
    {
        DatabaseReference userRef = dbReference.Child("users").Child(userId).Child("resources");

        userRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi tải tài nguyên từ cơ sở dữ liệu: {task.Exception}");
                callback?.Invoke(false);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    int loadedGolds = snapshot.Child("gold").Value != null ? int.Parse(snapshot.Child("gold").Value.ToString()) : 0;
                    int loadedDiamonds = snapshot.Child("diamonds").Value != null ? int.Parse(snapshot.Child("diamonds").Value.ToString()) : 0;

                    Debug.Log($"Tải thành công: Golds = {loadedGolds}, Diamonds = {loadedDiamonds}");
                    
                    // Cập nhật giá trị
                    unityContext.Post(_ =>
                    {
                        Golds.Value = loadedGolds;
                        Diamonds.Value = loadedDiamonds;
                    }, null);
                    callback?.Invoke(true);
                }
                else
                {
                    Debug.LogError("Tài nguyên không tồn tại trong cơ sở dữ liệu!");
                    callback?.Invoke(false);
                }
            }
        });
    }


    private void LoadResources()
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
                if (snapshot.Exists)
                {
                    Golds.Value = int.Parse(snapshot.Child("gold").Value.ToString());
                    Diamonds.Value = int.Parse(snapshot.Child("diamonds").Value.ToString());
                }
            }
        });
    }

    private void SaveResource(string resourceKey, int value)
    {
        dbReference.Child("users").Child(userId).Child("resources").Child(resourceKey).SetValueAsync(value).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi lưu tài nguyên {resourceKey}: {task.Exception}");
            }
            else
            {
                Debug.Log($"Đã lưu tài nguyên {resourceKey} với giá trị: {value}");
            }
        });
    }

    /// <summary>
    /// Thiết lập dữ liệu mặc định cho người dùng mới.
    /// </summary>
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

    /// <summary>
    /// Kiểm tra xem người dùng đã sở hữu spaceship hay chưa.
    /// </summary>
    public void CheckOwnedSpaceship(string userId, string shipId, Action<int> callback)
    {
        DatabaseReference shipRef = dbReference.Child("users").Child(userId).Child("ownedSpaceships").Child(shipId);

        shipRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi kiểm tra trạng thái sở hữu phi thuyền {shipId}: {task.Exception}");
                callback?.Invoke(0); // Trả về 0 nếu có lỗi
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int isOwned = (snapshot.Exists && snapshot.Value != null && snapshot.Value.ToString() == "1") ? 1 : 0;
                //Debug.Log($"Kiểm tra trạng thái sở hữu: {shipId}, Kết quả: {isOwned}");
                callback?.Invoke(isOwned);
            }
        });
    }


    /// <summary>
    /// Thêm spaceship vào danh sách sở hữu.
    /// </summary>
    public void AddSpaceshipToUser(string userId, string shipId, Action<bool> callback)
    {
        DatabaseReference shipRef = dbReference.Child("users").Child(userId).Child("ownedSpaceships").Child(shipId);

        shipRef.SetValueAsync(1).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi thêm phi thuyền {shipId}: {task.Exception}");
                callback?.Invoke(false);
            }
            else
            {
                Debug.Log($"Phi thuyền {shipId} đã được thêm vào tài khoản người dùng {userId}.");
                callback?.Invoke(true);
            }
        });
    }

    /// <summary>
    /// Cập nhật tài nguyên của người dùng.
    /// </summary>
    public void UpdateUserResources(string userId, int gold, int diamonds, Action<bool> callback)
    {
        var resources = new Dictionary<string, object>
        {
            { "gold", gold },
            { "diamonds", diamonds }
        };

        dbReference.Child("users").Child(userId).Child("resources").UpdateChildrenAsync(resources).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi cập nhật tài nguyên: {task.Exception}");
                callback?.Invoke(false);
            }
            else
            {
                Debug.Log("Tài nguyên đã được cập nhật thành công.");
                callback?.Invoke(true);
            }
        });
    }

    public void UpdateUserSettings(bool backgroundMusic, bool soundEffects, int bulletSoundVolume, Action<bool> callback)
    {
        var settings = new Dictionary<string, object>
        {
            { "backgroundMusic", backgroundMusic },
            { "soundEffects", soundEffects },
            { "bulletSoundVolume", bulletSoundVolume }
        };

        dbReference.Child("users").Child(userId).Child("settings").UpdateChildrenAsync(settings).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Lỗi khi cập nhật cài đặt người dùng: {task.Exception}");
                callback?.Invoke(false);
            }
            else
            {
                Debug.Log("Cập nhật cài đặt người dùng thành công.");
                callback?.Invoke(true);
            }
        });
    }

}