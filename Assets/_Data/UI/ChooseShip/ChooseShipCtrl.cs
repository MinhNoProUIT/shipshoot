using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseShipCtrl : BaseMonoBehaviour
{
    [SerializeField] protected ShipProfileSO shipProfileSO;
    //[SerializeField] protected TextMeshProUGUI hpText;
    //[SerializeField] protected TextMeshProUGUI dmgText;
    [SerializeField] protected TextMeshProUGUI shipName;
    [SerializeField] TextMeshProUGUI txtHp, txtSpeedGun, txtSpeedMove, txtDamage;

    [SerializeField] protected Image imgShip;
    [SerializeField] protected Button btnInfomation;
    [SerializeField] protected ShipShowDetail shipShowDetail;
    
    private SynchronizationContext unityContext;
    [SerializeField] protected LoadScene loadScene;

    protected override void Awake()
    {
        base.Awake();
        unityContext = SynchronizationContext.Current;

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //LoadHpText();
        //LoadDamageText();
        LoadShipName();
        LoadImgShip();
        LoadShipSO();
        LoadBtnInfomation();
        LoadShipShowDetail();
        LoadTextSpeedMove();
        LoadTextSpeedGun();
        LoadTextHP();
        LoadTextDamage();
        LoadLoadScene();
    }

    protected virtual void LoadLoadScene(){
        if (this.loadScene != null) return;
        this.loadScene = GameObject.Find("LoadSceneManager").GetComponentInChildren<LoadScene>();
        Debug.Log(transform.name + ": LoadLoadScene", gameObject);
    }

    protected virtual void LoadTextDamage()
    {
        if (this.txtDamage != null) return;
        this.txtDamage = transform.Find("Bottom").Find("Damage").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextDamage", gameObject);
    }
    protected virtual void LoadTextHP()
    {
        if (this.txtHp != null) return;
        this.txtHp = transform.Find("Bottom").Find("HP").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextHP", gameObject);
    }

    protected virtual void LoadTextSpeedGun()
    {
        if (this.txtSpeedGun != null) return;
        this.txtSpeedGun = transform.Find("Bottom").Find("SpeedGun").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextSpeedGun", gameObject);
    }

    protected virtual void LoadTextSpeedMove()
    {
        if (this.txtSpeedMove != null) return;
        this.txtSpeedMove = transform.Find("Bottom").Find("SpeedMove").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextSpeedMove", gameObject);
    }


    protected virtual void LoadShipShowDetail(){
        if(this.shipShowDetail!=null) return;
        this.shipShowDetail = GameObject.Find("ShipShowDetail").GetComponent<ShipShowDetail>();
        Debug.Log(transform.name + ": LoadShipShowDetail", gameObject);
    }

    protected virtual void LoadShipSO()
    {
        if (this.shipProfileSO != null) return;
        this.shipProfileSO = Resources.Load<ShipProfileSO>("Ship/" + transform.name);
        Debug.Log(transform.name + ": LoadShipSO", gameObject);
    }

    

   
   

    protected virtual void LoadBtnInfomation()
    {
        if (this.btnInfomation != null) return;
        this.btnInfomation = transform.Find("BtnInfomation").GetComponentInChildren<Button>();
       // btnBuyDiamonds.onClick.AddListener(OnBuyDiamondsClicked); // Thêm sự kiện cho nút mua bằng kim cương

        Debug.Log(transform.name + ": LoadBtnInfomation", gameObject);
    }



   

    protected virtual void LoadShipName()
    {
        if (this.shipName != null) return;
        this.shipName = transform.Find("SpaceshipName").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadHpText", gameObject);
    }

   
    protected virtual void LoadImgShip()
    {
        if (this.imgShip != null) return;
        this.imgShip = transform.Find("Bgr_Img_ship").Find("Image_ship").GetComponent<Image>();
        Debug.Log(transform.name + ": LoadInventory", gameObject);
    }

    private void OnShowInformationShipClicked(){
        if(this.shipShowDetail == null) return;

        shipShowDetail.SetShipProfileSO(shipProfileSO);

        shipShowDetail.gameObject.SetActive(true);

    }

    
   
    protected override void Start()
    {
        base.Start();
        if (shipProfileSO == null) return;
        Debug.Log("ShipProfileSO not null");
        AddListenerButton();

        LoadValue();

        AddTrigger();
    }

    protected virtual void AddTrigger(){
        EventTrigger forgotPasswordTrigger = this.imgShip.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry forgotEntry = new EventTrigger.Entry();
        forgotEntry.eventID = EventTriggerType.PointerClick;
        forgotEntry.callback.AddListener((data) => { OnGameObjectClicked(); });
        forgotPasswordTrigger.triggers.Add(forgotEntry);
    }

    protected virtual void OnGameObjectClicked(){
        PlayerPrefs.SetString("SHIPNAME", transform.name);
        PlayerPrefs.Save();

        if(loadScene!= null) this.loadScene.LoadSceneAsync(PlayerPrefs.GetString("LEVELCURRENT", "GamePlay"));
        //SceneManager.LoadSceneAsync("GamePlay");
    }

    protected virtual void AddListenerButton(){
        AddListenerButtonInformationShip();
    }

    protected virtual void AddListenerButtonInformationShip(){
        if (btnInfomation == null)
        {
            Debug.LogError("btnBuyCoins is null. Cannot add listener.");
        }
        else
        {
            btnInfomation.onClick.AddListener(OnShowInformationShipClicked);
            Debug.Log("Added OnBuyCoinsClicked listener.");
        }
    }



    protected virtual void LoadValue()
    {
        this.imgShip.sprite = shipProfileSO.sprite;
        this.shipName.text = shipProfileSO.ShipName;
        this.txtDamage.text ="Damage: "+ this.shipProfileSO.dameMax.ToString();
        this.txtHp.text ="HP: "+ this.shipProfileSO.hpMax.ToString();
        this.txtSpeedGun.text ="Speed Gun:" + this.shipProfileSO.speedGun.ToString();
        this.txtSpeedMove.text = "Speed Move: " + this.shipProfileSO.speedMovement.ToString();
        if(DatabaseManager.Instance != null) Debug.Log("Database Manager not null");
        Debug.Log(PlayerPrefs.GetString("UserId", ""));
        Debug.Log(transform.name);

        CheckOwnedSpaceshipCurrent();

    }

    public virtual void CheckOwnedSpaceshipCurrent(){
        DatabaseManager.Instance.CheckOwnedSpaceship(PlayerPrefs.GetString("UserId", ""), transform.name, isOwned =>
        {
            unityContext.Post(_ =>
            {
                //Debug.Log($"Ket qua tra ve isOwned voi ship {transform.name} la isOwned la {isOwned}");
                if(isOwned == 0) gameObject.SetActive(false);
            }, null);
        });
    }

}
