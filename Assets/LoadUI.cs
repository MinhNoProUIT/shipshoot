using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadUI : BaseMonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]protected TextMeshProUGUI time, gold, diamond;
    [SerializeField] protected StarManager starManager;
    // [SerializeField]Button replay, home, chooseMenu;
    protected override void Awake()
    {
        base.Awake();
        /* PlayerPrefs.SetInt("Gold", 50);
        PlayerPrefs.SetInt("Diamonds", 5);
        PlayerPrefs.SetInt("CountdownTime", 150);
        PlayerPrefs.Save(); */
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTime();
        LoadCoin();
        LoadDiamonds();
        LoadStarManager();
    }

    protected virtual void LoadStarManager()
    {
        if (this.starManager != null) return;
        this.starManager = transform.GetComponentInChildren<StarManager>();
        Debug.LogWarning(transform.name + ": LoadStarManager", gameObject);
    }

    protected virtual void LoadTime()
    {
        if (this.time != null) return;
        this.time = transform.Find("Center").Find("Time").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTime", gameObject);
    }
    protected virtual void LoadCoin()
    {
        if (this.gold != null) return;
        this.gold = transform.Find("Center").Find("Coin").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadCoin", gameObject);
    }
    protected virtual void LoadDiamonds()
    {
        if (this.diamond != null) return;
        this.diamond = transform.Find("Center").Find("Diamonds").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadDiamonds", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        LoadText();
        UpdateDatabase();
    }

    protected virtual void UpdateDatabase(){

        int gold = PlayerPrefs.GetInt("Gold", 10);
        int diamonds = PlayerPrefs.GetInt("Diamonds", 1);

        DatabaseManager.Instance.Golds.Value += gold;
        DatabaseManager.Instance.Diamonds.Value += diamonds;

        Debug.Log("Đã thêm vàng thành công!");
        DatabaseManager.Instance.AddResource("gold", gold, success =>
        {
            if (success)
            {
                Debug.Log("Đã thêm vàng thành công!");
            }
            else
            {
                Debug.LogError("Không thể thêm vàng!");
            }
        });

        DatabaseManager.Instance.AddResource("diamonds", diamonds, success =>
        {
            if (success)
            {
                Debug.Log("Đã thêm kim cương thành công!");
            }
            else
            {
                Debug.LogError("Không thể thêm kim cương!");
            }
        });
    }

    protected virtual void LoadText(){
        int timer = PlayerPrefs.GetInt("CountdownTime", 0);
        int minutes = timer / 60; // Lấy số phút
        int seconds = timer % 60; // Lấy số giây còn lại

        string timeString ="Completion time: "+ $"{minutes:00}:{seconds:00}";
        time.text = timeString;

        gold.text = PlayerPrefs.GetInt("Gold", 0).ToString();
        diamond.text = PlayerPrefs.GetInt("Diamonds", 0).ToString();

    }

    public int GetStarByTime(){
        string gameResult = PlayerPrefs.GetString("GAMERESULT", "Win");
        if(gameResult == "Lose") return 0;

        int timer = PlayerPrefs.GetInt("CountdownTime", 0);
        Debug.Log(transform.name+": "  + timer);
        if(timer>180) return 3;
        else if(timer>120) return 2;
        else if(timer > 0) return 1;
        return 0;
    }

    public void Replay(){
        string levelCurrent = PlayerPrefs.GetString("LEVELCURRENT", "Level1");
        SceneManager.LoadSceneAsync(levelCurrent);

    }

    public void Home(){
        SceneManager.LoadScene("MainScene");
    }

    public void Level(){
        SceneManager.LoadScene("Choose Menu");
    }

}
