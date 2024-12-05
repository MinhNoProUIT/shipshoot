using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUICtrl : BaseMonoBehaviour
{
    [SerializeField] protected ShipProfileSO shipProfileSO;
    [SerializeField] protected TextMeshProUGUI hpText;
    [SerializeField] protected TextMeshProUGUI dmgText;
    [SerializeField] protected TextMeshProUGUI levelUnlock;
    [SerializeField] protected Image imgShip;
    [SerializeField] protected Button btnBuyCoins;
    [SerializeField] protected Button btnBuyDiamonds;
    [SerializeField] protected TextMeshProUGUI btnBuyCoinsText;
    [SerializeField] protected TextMeshProUGUI btnBuyDiamondsText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHpText();
        LoadDamageText();
        LoadImgShip();
        LoadLevelUnlock();
        LoadBtnBuyCoins();
        LoadBtnBuyDiamonds();
        LoadBtnBuyCoinsText();
        LoadBtnBuyDiamondsText();
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

    protected virtual void LoadLevelUnlock()
    {
        if (this.levelUnlock != null) return;
        this.levelUnlock = transform.Find("LevelUnlock").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadLevelUnlock", gameObject);
    }

    protected virtual void LoadHpText()
    {
        if (this.hpText != null) return;
        this.hpText = transform.Find("Dame_HP").Find("HP").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadHpText", gameObject);
    }

    protected virtual void LoadDamageText()
    {
        if (this.dmgText != null) return;
        this.dmgText = transform.Find("Dame_HP").Find("Damage").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadInventory", gameObject);
    }

    protected virtual void LoadImgShip()
    {
        if (this.imgShip != null) return;
        this.imgShip = transform.Find("Image_ship").GetComponent<Image>();
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
        this.hpText.text = "HP: " + shipProfileSO.hpMax.ToString();
        this.dmgText.text = "DMG: " + shipProfileSO.dameMax.ToString();
        this.imgShip.sprite = shipProfileSO.sprite;
        this.btnBuyCoinsText.text = shipProfileSO.coins.ToString();
        this.btnBuyDiamondsText.text = shipProfileSO.diamonds.ToString();
        this.levelUnlock.text = "Cấp độ mở khóa" + shipProfileSO.levelUnlock.ToString();
    }
}
