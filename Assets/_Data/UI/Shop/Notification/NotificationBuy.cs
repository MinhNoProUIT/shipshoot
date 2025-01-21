using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBuy : BaseMonoBehaviour
{
    [SerializeField] ShipProfileSO shipProfileSO;
    [SerializeField] ShipUICtrl shipUICtrl;
    [SerializeField] protected Image imgShip, background;
    [SerializeField] Button btnYes, btnNo;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] protected Notification_Congratulation notification_Congratulation;
    [SerializeField] protected NotificationLose notificationLose;
    [SerializeField] protected int buyMoney = 0;
    SynchronizationContext unityContext;

    public void SetShipProfileSO(ShipProfileSO shipProfileSO){
        this.shipProfileSO = shipProfileSO;
    }

    public int SetBuyMoney(int money){
        this.buyMoney = money;
        return this.buyMoney;
    }

    protected override void Awake()
    {
        base.Awake();
        unityContext = SynchronizationContext.Current;

    }

    protected override void Start()
    {
        base.Start();
        AddListenerBtnNo();
        AddListenerBtnYes();
    }

    protected virtual void AddListenerBtnYes(){
        if (btnYes == null)
        {
            Debug.LogError("btnClose is null. Cannot add listener.");
        }
        else
        {
            btnYes.onClick.AddListener(OnYesClick);
            Debug.Log("Added OnYesClick listener.");
        }
    }

    protected virtual void OnYesClick(){
        if(this.buyMoney == 0) BuyShipByCoin();
        else if(this.buyMoney == 1) BuyShipByDiamonds();
    }

    protected virtual void BuyShipByCoin()
    {
        if (DatabaseManager.Instance.Golds.Value >= shipProfileSO.coins)
        {
            // Cập nhật số vàng
            DatabaseManager.Instance.Golds.Value -= shipProfileSO.coins;

            Debug.LogWarning(this.shipProfileSO.ShipId);
            //this.shipUICtrl = GameObject.Find(this.shipProfileSO.ShipId.ToString()).GetComponent<ShipUICtrl>();

            // Sử dụng Coroutine để đảm bảo tiến trình hoàn thành trước khi xử lý giao diện
            StartCoroutine(UpdateDatabaseAndShowNotification());
        }
        else
        {
            Debug.Log("Không đủ vàng để mua phi thuyền.");
            this.notificationLose.SetShipProfileSO(shipProfileSO);
            this.notificationLose.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator UpdateDatabaseAndShowNotification()
    {
        bool success = false;

        DatabaseManager.Instance.AddSpaceshipToUser(DatabaseManager.Instance.GetUserId(), shipProfileSO.ShipId.ToString(), result =>
        {
            success = result;
        });

        // Chờ cho đến khi kết quả cập nhật xong
        yield return new WaitUntil(() => success);

        if (success)
        {
            Debug.Log("Mua phi thuyền bằng vàng thành công!");
            this.shipUICtrl.CheckOwnedSpaceshipCurrent();
            this.notification_Congratulation.SetShipProfileSO(shipProfileSO);
            this.notification_Congratulation.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Lỗi khi cập nhật database.");
        }
    }


    protected virtual void BuyShipByDiamonds()
    {
        if (DatabaseManager.Instance.Diamonds.Value >= shipProfileSO.diamonds)
        {
            // Cập nhật số vàng
            DatabaseManager.Instance.Diamonds.Value -= shipProfileSO.diamonds;

            Debug.LogWarning(this.shipProfileSO.ShipId);
            //this.shipUICtrl = GameObject.Find(this.shipProfileSO.ShipId.ToString()).GetComponent<ShipUICtrl>();

            // Sử dụng Coroutine để đảm bảo tiến trình hoàn thành trước khi xử lý giao diện
            StartCoroutine(UpdateDatabaseAndShowNotification());
        }
        else
        {
            Debug.Log("Không đủ kim cương để mua phi thuyền.");
            this.notificationLose.SetShipProfileSO(shipProfileSO);
            this.notificationLose.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    protected virtual void AddListenerBtnNo(){
        if (btnNo == null)
        {
            Debug.LogError("btnClose is null. Cannot add listener.");
        }
        else
        {
            btnNo.onClick.AddListener(OnNoClicked);
            Debug.Log("Added OnNoClicked listener.");
        }
    }

    protected virtual void OnNoClicked()
    {
        // Đảm bảo GameObject đang hoạt động
        if (!gameObject.activeSelf) return;

        // Tạo một chuỗi hiệu ứng
        Sequence sequence = DOTween.Sequence();

        // Bước 1: Tăng scale từ (1, 1, 1) lên (1.2, 1.2, 1.2) trong 0.5 giây
        //sequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).SetEase(Ease.OutQuad));

        // Bước 2: Giảm scale từ (1.2, 1.2, 1.2) về (0, 0, 0) trong 2 giây
        sequence.Append(background.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack));

        // Bước 3: Tắt GameObject sau khi hoàn tất
        sequence.OnComplete(() => gameObject.SetActive(false));
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadImgShip();
        LoadTextDescription();
        LoadBtnYes();
        LoadBtnClose();
        LoadBackground();
        LoadNotification_Congratulation();
        LoadNotificationLose();
        LoadShipCtrl();
    }

    protected virtual void LoadShipCtrl(){
        //if(this.shipUICtrl!=null) return;
        this.shipUICtrl = GameObject.Find(this.shipProfileSO.ShipId.ToString()).GetComponent<ShipUICtrl>();
        Debug.Log(transform.name + ": LoadShipCtrl", gameObject);
    }

    protected virtual void LoadNotificationLose(){
        if(this.notificationLose!=null) return;
        this.notificationLose = GameObject.Find("Notification_Lose").GetComponent<NotificationLose>();
        Debug.Log(transform.name + ": LoadNotificationLose", gameObject);
    }
    

    protected virtual void LoadNotification_Congratulation(){
        if(this.notification_Congratulation!=null) return;
        this.notification_Congratulation = GameObject.Find("Notification_Congratulation").GetComponent<Notification_Congratulation>();
        Debug.Log(transform.name + ": LoadNotification_Congratulation", gameObject);
    }

    protected virtual void LoadBtnYes(){
        if(this.btnYes != null) return;
        this.btnYes = transform.Find("Background").Find("Bottom").Find("Yes").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadBtnYes", gameObject);
    }

    protected virtual void LoadBtnClose(){
        if(this.btnNo != null) return;
        this.btnNo = transform.Find("Background").Find("Bottom").Find("No").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadBtnClose", gameObject);
    }

    protected virtual void LoadTextDescription()
    {
        if (this.content != null) return;
        this.content = transform.Find("Background").Find("Middle").Find("Notification_Content").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextDescription", gameObject);
    }

    protected virtual void LoadImgShip()
    {
        if (this.imgShip != null) return;
        this.imgShip = transform.Find("Background").Find("Middle").Find("Image").GetComponent<Image>();
        Debug.Log(transform.name + ": LoadImgShip", gameObject);
    }

    protected virtual void LoadBackground()
    {
        if (this.background != null) return;
        this.background = transform.Find("Background").GetComponent<Image>();
        Debug.Log(transform.name + ": LoadBackground", gameObject);
    }

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        //SetShipProfileSO();
        EffectDotween();
        LoadShipCtrl();

        SetInformationShip();
    }
    
    protected virtual void EffectDotween(){
        background.transform.localScale = Vector3.zero;

        background.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
    }

    protected virtual void SetInformationShip(){
        if(shipProfileSO == null) return;
        Debug.Log("ShipProfileSO not null");

        this.imgShip.sprite = this.shipProfileSO.sprite;
        this.content.text = $"Do you want to buy '{this.shipProfileSO.ShipName}' spacecraft?";
        
    }
}
