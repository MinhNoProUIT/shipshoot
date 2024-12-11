using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : BaseMonoBehaviour
{
    [SerializeField] protected List<Button> levelButtons;
    [SerializeField] protected int unlockLevel = 1;
    protected override void LoadComponents(){
        base.LoadComponents();
        this.LoadLevelButtons();
    }

    protected virtual void LoadLevelButtons(){
        if(levelButtons.Count>0) return;
        foreach(Transform child in transform){
            this.levelButtons.Add(child.GetComponent<Button>());
        }
    }
    protected override void Awake()
    {
        base.Awake();
        unlockLevel = PlayerPrefs.GetInt("UnlockLevel", 1);
        LoadButtonInteractable();
    }

    protected virtual void LoadButtonInteractable(){
        for(int i = 0; i < levelButtons.Count; i++){
            levelButtons[i].interactable = false;
        }
        for(int i = 0; i < unlockLevel; i++){
            levelButtons[i].interactable = true;
        }
    }
}

