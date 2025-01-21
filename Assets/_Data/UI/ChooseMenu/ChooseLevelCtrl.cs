using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelCtrl : BaseMonoBehaviour
{
    [SerializeField] protected LoadScene loadScene;
    [SerializeField] protected Button levelButton;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
        LoadLoadScene();
    }

    protected virtual void LoadLoadScene(){
        if (this.loadScene != null) return;
        this.loadScene = GameObject.Find("Botttom").GetComponent<LoadScene>();
        Debug.Log(transform.name + ": LoadLoadScene", gameObject);
    }

    protected virtual void LoadButton(){
        if (this.levelButton != null) return;
        this.levelButton = transform.GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        levelButton.onClick.AddListener(OnLevelButtonClicked);
    }

    protected virtual void OnLevelButtonClicked(){
        PlayerPrefs.SetString("LEVELCURRENT", transform.name);
        PlayerPrefs.Save();

        if(loadScene!= null) this.loadScene.LoadSceneAsync("ChooseShip");
    }
}
