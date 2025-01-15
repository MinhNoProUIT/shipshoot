using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField] protected Button btnInfomation;
    [SerializeField] protected Button btnisOwned;
    [SerializeField] protected TextMeshProUGUI btnBuyCoinsText;
    [SerializeField] protected TextMeshProUGUI btnBuyDiamondsText;
    [SerializeField] protected ShipShowDetail shipShowDetail;
    private SynchronizationContext unityContext;

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
        LoadLevelUnlock();
        LoadBtnBuyCoins();
        LoadBtnBuyDiamonds();
        LoadBtnBuyCoinsText();
        LoadBtnIsOwned();
        LoadBtnBuyDiamondsText();
        LoadShipSO();
        LoadBtnInfomation();
        LoadShipShowDetail();
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
       // btnBuyDiamonds.onClick.AddListener(OnBuyDiamondsClicked); // Thêm sự kiện cho nút mua bằng kim cương

        Debug.Log(transform.name + ": LoadBtnBuyDiamonds", gameObject);
    }

    protected virtual void LoadBtnBuyCoins()
    {
        if (this.btnBuyCoins != null) return;
        this.btnBuyCoins = transform.Find("BtnBuy").Find("BtnBuyCoins").GetComponentInChildren<Button>();
        
        Debug.Log(transform.name + ": LoadBtnBuyCoins", gameObject);
    }

    protected virtual void LoadBtnInfomation()
    {
        if (this.btnInfomation != null) return;
        this.btnInfomation = transform.Find("BtnInfomation").GetComponentInChildren<Button>();
       // btnBuyDiamonds.onClick.AddListener(OnBuyDiamondsClicked); // Thêm sự kiện cho nút mua bằng kim cương

        Debug.Log(transform.name + ": LoadBtnInfomation", gameObject);
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

    private void OnShowInformationShipClicked(){
        if(this.shipShowDetail == null) return;

        shipShowDetail.SetShipProfileSO(shipProfileSO);

        shipShowDetail.gameObject.SetActive(true);

    }

    private void OnBuyCoinsClicked()
    {
        //Debug.Log("So vang hien tai la: "+DatabaseManager.Instance.Golds.Value);
        if (DatabaseManager.Instance.Golds.Value >= shipProfileSO.coins)
        {
            // Cập nhật số vàng
            DatabaseManager.Instance.Golds.Value -= shipProfileSO.coins;
            // Cập nhật phi thuyền sở hữu
            DatabaseManager.Instance.AddSpaceshipToUser(DatabaseManager.Instance.GetUserId(), shipProfileSO.ShipId.ToString(), success =>
            {
                if (success)
                {
                    Debug.Log("Mua phi thuyền bằng vàng thành công!");
                    CheckOwnedSpaceshipCurrent();
                    // Cập nhật UI hoặc thực hiện hành động khác nếu cần
                }
            });
        }
        else
        {
            Debug.Log("Không đủ vàng để mua phi thuyền.");
        }
    }

    private void OnBuyDiamondsClicked()
    {
        if (DatabaseManager.Instance.Diamonds.Value >= shipProfileSO.diamonds)
        {
            // Cập nhật số kim cương
            DatabaseManager.Instance.Diamonds.Value -= shipProfileSO.diamonds;
            // Cập nhật phi thuyền sở hữu
            DatabaseManager.Instance.AddSpaceshipToUser(DatabaseManager.Instance.GetUserId(), shipProfileSO.ShipId.ToString(), success =>
            {
                if (success)
                {
                    Debug.Log("Mua phi thuyền bằng kim cương thành công!");
                    CheckOwnedSpaceshipCurrent();
                    // Cập nhật UI hoặc thực hiện hành động khác nếu cần
                }
            });
        }
        else
        {
            Debug.Log("Không đủ kim cương để mua phi thuyền.");
        }
    }

    protected override void Start()
    {
        base.Start();
        if (shipProfileSO == null) return;
        Debug.Log("ShipProfileSO not null");
        AddListenerButton();

        LoadValue();
    }

    protected virtual void AddListenerButton(){
        AddListenerButtonBuyCoin();
        AddListenerButtonBuyDiamond();
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


    protected virtual void AddListenerButtonBuyCoin(){
        if (btnBuyCoins == null)
        {
            Debug.LogError("btnBuyCoins is null. Cannot add listener.");
        }
        else
        {
            btnBuyCoins.onClick.AddListener(OnBuyCoinsClicked);
            Debug.Log("Added OnBuyCoinsClicked listener.");
        }
    }

    protected virtual void AddListenerButtonBuyDiamond(){
        if (btnBuyCoins == null)
        {
            Debug.LogError("btnBuyDiamonds is null. Cannot add listener.");
        }
        else
        {
            btnBuyDiamonds.onClick.AddListener(OnBuyDiamondsClicked);
            Debug.Log("Added OnBuyDiamondsClicked listener.");
        }
    }

    protected virtual void LoadValue()
    {
        this.imgShip.sprite = shipProfileSO.sprite;
        this.btnBuyCoinsText.text = shipProfileSO.coins.ToString();
        this.btnBuyDiamondsText.text = shipProfileSO.diamonds.ToString();
        this.levelUnlock.text = "Level unlock: " + shipProfileSO.levelUnlock.ToString();
        this.shipName.text = shipProfileSO.ShipName;

        if(DatabaseManager.Instance != null) Debug.Log("Database Manager not null");
        Debug.Log(PlayerPrefs.GetString("UserId", ""));
        Debug.Log(transform.name);

        CheckOwnedSpaceshipCurrent();
    }

    protected virtual void CheckOwnedSpaceshipCurrent(){
        DatabaseManager.Instance.CheckOwnedSpaceship(PlayerPrefs.GetString("UserId", ""), transform.name, isOwned =>
        {
            unityContext.Post(_ =>
            {
                //Debug.Log($"Ket qua tra ve isOwned voi ship {transform.name} la isOwned la {isOwned}");
                SetActiveButtons(isOwned);
                RefreshUI();
                Debug.Log("Da cap nhat button");
            }, null);
        });
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

    protected virtual void RefreshUI()
    {
        btnBuyCoins.gameObject.SetActive(!btnisOwned.gameObject.activeSelf);
        btnBuyDiamonds.gameObject.SetActive(!btnisOwned.gameObject.activeSelf);
        Debug.Log("Giao diện đã được làm mới.");
    }

}
