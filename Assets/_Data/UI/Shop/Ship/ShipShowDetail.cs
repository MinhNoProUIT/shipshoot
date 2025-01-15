using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipShowDetail : BaseMonoBehaviour
{
    [SerializeField] ShipProfileSO shipProfileSO;
    [SerializeField] protected Image imgShip, background;
    [SerializeField] Button btnClose;
    [SerializeField] TextMeshProUGUI txtShipName, txtHp, txtSpeedGun, txtSpeedMove, txtDamage, txtDecription;
    public void SetShipProfileSO(ShipProfileSO shipProfileSO){
        this.shipProfileSO = shipProfileSO;
    }

    protected override void Start()
    {
        base.Start();
        AddListenerBtnClose();
    }

    protected virtual void AddListenerBtnClose(){
        if (btnClose == null)
        {
            Debug.LogError("btnClose is null. Cannot add listener.");
        }
        else
        {
            btnClose.onClick.AddListener(OnCloseShowDetailClicked);
            Debug.Log("Added OnCloseShowDetailClicked listener.");
        }
    }

    protected virtual void OnCloseShowDetailClicked()
    {
        // Đảm bảo GameObject đang hoạt động
        if (!gameObject.activeSelf) return;

        // Tạo một chuỗi hiệu ứng
        Sequence sequence = DOTween.Sequence();

        // Bước 1: Tăng scale từ (1, 1, 1) lên (1.2, 1.2, 1.2) trong 0.5 giây
        //sequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f).SetEase(Ease.OutQuad));

        // Bước 2: Giảm scale từ (1.2, 1.2, 1.2) về (0, 0, 0) trong 2 giây
        sequence.Append(background.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InBack));

        // Bước 3: Tắt GameObject sau khi hoàn tất
        sequence.OnComplete(() => gameObject.SetActive(false));
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadImgShip();
        LoadTextShipName();
        LoadTextDescription();
        LoadTextSpeedMove();
        LoadTextSpeedGun();
        LoadTextHP();
        LoadTextDamage();
        LoadBtnClose();
        LoadBackground();

    }

    protected virtual void LoadBtnClose(){
        if(this.btnClose != null) return;
        this.btnClose = transform.Find("Background").Find("TopRight").Find("btnClose").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadBtnClose", gameObject);
    }

    protected virtual void LoadTextDamage()
    {
        if (this.txtDamage != null) return;
        this.txtDamage = transform.Find("Background").Find("TopRight").Find("Content").Find("Damage").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextDamage", gameObject);
    }
    protected virtual void LoadTextHP()
    {
        if (this.txtHp != null) return;
        this.txtHp = transform.Find("Background").Find("TopRight").Find("Content").Find("HP").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextHP", gameObject);
    }

    protected virtual void LoadTextSpeedGun()
    {
        if (this.txtSpeedGun != null) return;
        this.txtSpeedGun = transform.Find("Background").Find("TopRight").Find("Content").Find("SpeedGun").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextSpeedGun", gameObject);
    }

    protected virtual void LoadTextSpeedMove()
    {
        if (this.txtSpeedMove != null) return;
        this.txtSpeedMove = transform.Find("Background").Find("TopRight").Find("Content").Find("SpeedMove").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextSpeedMove", gameObject);
    }

    protected virtual void LoadTextDescription()
    {
        if (this.txtDecription != null) return;
        this.txtDecription = transform.Find("Background").Find("Bottom").Find("Description Content").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextDescription", gameObject);
    }

    protected virtual void LoadTextShipName()
    {
        if (this.txtShipName != null) return;
        this.txtShipName = transform.Find("Background").Find("Top").Find("Ship Title").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextShipName", gameObject);
    }

    protected virtual void LoadImgShip()
    {
        if (this.imgShip != null) return;
        this.imgShip = transform.Find("Background").Find("TopLeft").Find("Ship").GetComponent<Image>();
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

        background.transform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutBack);
    }

    protected virtual void SetInformationShip(){
        if(shipProfileSO == null) return;
        Debug.Log("ShipProfileSO not null");

        this.imgShip.sprite = this.shipProfileSO.sprite;
        this.txtDecription.text = this.shipProfileSO.Description;
        this.txtDamage.text ="Damage: "+ this.shipProfileSO.dameMax.ToString();
        this.txtHp.text ="HP: "+ this.shipProfileSO.hpMax.ToString();
        this.txtSpeedGun.text ="Speed Gun:" + this.shipProfileSO.speedGun.ToString();
        this.txtSpeedMove.text = "Speed Move: " + this.shipProfileSO.speedMovement.ToString();
        this.txtShipName.text = this.shipProfileSO.ShipName;
    }
}
