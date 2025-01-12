using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUICtrl : BaseMonoBehaviour
{
    [SerializeField] protected ShipProfileSO shipProfileSO;
    //[SerializeField] protected TextMeshProUGUI hpText;
    //[SerializeField] protected TextMeshProUGUI dmgText;
    [SerializeField] protected TextMeshProUGUI levelUnlock;
    [SerializeField] protected TextMeshProUGUI shipName;
    [SerializeField] protected Image imgShip;
    [SerializeField] protected Button btnBuyCoins;
    [SerializeField] protected Button btnBuyDiamonds;
    [SerializeField] protected Button btnisOwned;
    [SerializeField] protected TextMeshProUGUI btnBuyCoinsText;
    [SerializeField] protected TextMeshProUGUI btnBuyDiamondsText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        //LoadHpText();
        //LoadDamageText();
        LoadShipName();
        LoadImgShip();
        LoadLevelUnlock();
        LoadBtnBuyCoins();
        LoadBtnBuyDiamonds();
        LoadBtnBuyCoinsText();
        LoadBtnIsOwned();
        LoadBtnBuyDiamondsText();
        LoadShipSO();
    }

    protected virtual void LoadShipSO()
    {
        if (this.shipProfileSO != null) return;
        this.shipProfileSO = Resources.Load<ShipProfileSO>("Ship/" + transform.name);
        Debug.Log(transform.name + ": LoadShipSO", gameObject);
    }

    protected virtual void LoadBtnBuyDiamondsText()
    {
        if (this.btnBuyDiamondsText != null) return;
        this.btnBuyDiamondsText = transform.Find("BtnBuy").Find("BtnBuyDiamonds").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadBtnBuyDiamonds", gameObject);
    }

    protected virtual void LoadBtnBuyCoinsText()
    {
        if (this.btnBuyCoinsText != null) return;
        this.btnBuyCoinsText = transform.Find("BtnBuy").Find("BtnBuyCoins").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadBtnBuyDiamonds", gameObject);
    }

    protected virtual void LoadBtnBuyDiamonds()
    {
        if (this.btnBuyDiamonds != null) return;
        this.btnBuyDiamonds = transform.Find("BtnBuy").Find("BtnBuyDiamonds").GetComponentInChildren<Button>();
        Debug.Log(transform.name + ": LoadBtnBuyDiamonds", gameObject);
    }

    protected virtual void LoadBtnBuyCoins()
    {
        if (this.btnBuyCoins != null) return;
        this.btnBuyCoins = transform.Find("BtnBuy").Find("BtnBuyCoins").GetComponentInChildren<Button>();
        Debug.Log(transform.name + ": LoadBtnBuyCoins", gameObject);
    }

    protected virtual void LoadBtnIsOwned()
    {
        if (this.btnisOwned != null) return;
        this.btnisOwned = transform.Find("BtnIsOwned").GetComponentInChildren<Button>();
        Debug.Log(transform.name + ": LoadBtnIsOwned", gameObject);
    }


    protected virtual void LoadLevelUnlock()
    {
        if (this.levelUnlock != null) return;
        this.levelUnlock = transform.Find("LevelUnlock").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadLevelUnlock", gameObject);
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

    protected override void Start()
    {
        base.Start();
        if (shipProfileSO == null) return;
        Debug.Log("ShipProfileSO not null");
        LoadValue();
    }

    protected virtual void LoadValue()
    {
        this.imgShip.sprite = shipProfileSO.sprite;
        this.btnBuyCoinsText.text = shipProfileSO.coins.ToString();
        this.btnBuyDiamondsText.text = shipProfileSO.diamonds.ToString();
        this.levelUnlock.text = "Level unlock: " + shipProfileSO.levelUnlock.ToString();
        this.shipName.text = shipProfileSO.ShipName;
    }

    public virtual void SetActiveButtons(int isOwned){
        if(isOwned == 1){
            btnBuyCoins.gameObject.SetActive(false);
            btnBuyDiamonds.gameObject.SetActive(false);
            btnisOwned.gameObject.SetActive(true);
        }
        else
        {
            btnBuyCoins.gameObject.SetActive(true);
            btnBuyDiamonds.gameObject.SetActive(true);
            btnisOwned.gameObject.SetActive(false);
        }
    }
}
