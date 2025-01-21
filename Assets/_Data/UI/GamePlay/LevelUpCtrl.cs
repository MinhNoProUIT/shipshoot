using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpCtrl : BaseMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txtLevelUp;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextLevelUp();
    }

    protected virtual void LoadTextLevelUp(){
        if (this.txtLevelUp != null) return;
        this.txtLevelUp = transform.Find("Background").Find("txtLevelUp").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextLevelUp", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        txtLevelUp.text = "LEVEL "+ LevelByKillEnemy.Instance.LevelCurrent.ToString();
    }

}
