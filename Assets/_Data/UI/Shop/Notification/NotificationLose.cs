using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationLose : BaseMonoBehaviour
{
    [SerializeField] ShipProfileSO shipProfileSO;
    [SerializeField] protected Image imgShip, background;
    [SerializeField] Button btnOk;
    [SerializeField] TextMeshProUGUI content;
    public void SetShipProfileSO(ShipProfileSO shipProfileSO){
        this.shipProfileSO = shipProfileSO;
    }

    protected override void Start()
    {
        base.Start();
        AddListenerBtnOk();
    }

    protected virtual void AddListenerBtnOk(){
        if (btnOk == null)
        {
            Debug.LogError("btnOk is null. Cannot add listener.");
        }
        else
        {
            btnOk.onClick.AddListener(OnOkClick);
            Debug.Log("Added OnOkClick listener.");
        }
    }

    protected virtual void OnOkClick()
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
        LoadBtnOk();
        LoadBackground();
    }


    protected virtual void LoadBtnOk(){
        if(this.btnOk != null) return;
        this.btnOk = transform.Find("Background").Find("Bottom").Find("Ok").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadBtnOk", gameObject);
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
        this.content.text = $"Congratulations on purchasing the {this.shipProfileSO.ShipName} spacecraft.?";
        
    }
}
