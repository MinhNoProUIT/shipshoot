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
    [SerializeField]TextMeshProUGUI time, gold, diamond;
   // [SerializeField]Button replay, home, chooseMenu;

    protected override void Start()
    {
        base.Start();
        LoadText();
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

    public void Replay(){
        SceneManager.LoadScene("Gameplay");

    }

    public void Home(){
        SceneManager.LoadScene("MainScene");
    }

    public void Level(){
        SceneManager.LoadScene("Choose Menu");
    }

}
