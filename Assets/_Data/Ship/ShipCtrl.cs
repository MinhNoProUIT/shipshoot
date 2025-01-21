using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCtrl : BaseMonoBehaviour
{
    [Header("Ship")]
    [SerializeField] protected SpriteRenderer model;
    public SpriteRenderer Model => model;

    [SerializeField] protected ShipShootByMouse objShooting;
    public ShipShootByMouse ObjShooting => objShooting;
    [SerializeField] protected ShipBoundaryScreen objMove;
    public ShipBoundaryScreen ObjMove => objMove;

    [SerializeField] protected ShipDamageReceiver damageReceiver;
    public ShipDamageReceiver DamageReceiver => damageReceiver;

    [SerializeField] protected Inventory inventory;
    public Inventory Inventory => inventory;
    //[SerializeField] protected Slider slider;
    [SerializeField] protected ShipProfileSO shipProfileSO;
    public ShipProfileSO ShipProfileSO => shipProfileSO;


    //protected virtual void Update()
    //{
    //    if (slider == null) return;
    //    slider.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}
    
    protected override void LoadComponents()
    {
        this.LoadModel();
        this.LoadObjShooting();
        this.LoadDameReceiver();

        base.LoadComponents();
        this.LoadInventory();
        this.LoadObjMovement();
        //this.LoadSlider();
    }

    protected virtual void LoadObjMovement()
    {
        if (this.objMove != null) return;
        this.objMove = GetComponentInChildren<ShipBoundaryScreen>();
        Debug.LogWarning(transform.name + ": LoadObjShooting", gameObject);
    }

    protected virtual void LoadObjShooting()
    {
        if (this.objShooting != null) return;
        this.objShooting = GetComponentInChildren<ShipShootByMouse>();
        Debug.LogWarning(transform.name + ": LoadObjShooting", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

     protected virtual void LoadDameReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.GetComponentInChildren<ShipDamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadDameReceiver", gameObject);
    }


    //protected void LoadSlider()
    //{
    //    if (this.slider != null) return;
    //    this.slider = transform.GetComponentInChildren<Slider>();
    //    Debug.Log(transform.name + ": LoadSlider", gameObject);
    //}

    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = transform.GetComponentInChildren<Inventory>();
        Debug.Log(transform.name + ": LoadInventory", gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        LoadShipProfile();
    }

    protected virtual void LoadShipProfile(){
        //Con code tiep
        string shipname = PlayerPrefs.GetString("SHIPNAME", "SH001");
        string resPath = "Ship/" + shipname;
        this.shipProfileSO = Resources.Load<ShipProfileSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadSpecialSO " + resPath, gameObject);
    }

    protected override void Start()
    {
        base.Start();
        if(shipProfileSO==null) return;
        this.model.sprite = shipProfileSO.sprite;
        CreateHPBar();
    }

    public virtual void CreateHPBar(){
        //S shootableObject = newEnemy.GetComponent<ShootableObjectCtrl>();
        //if (shootableObject == null) return;
        
        Transform newHPBar = HPBarSpawner.Instance.Spawn(HPBarSpawner.HPBar, transform.position, Quaternion.identity);

        HPBar HPBar = newHPBar.GetComponent<HPBar>();
        if (HPBar == null) return;

        HPBar.SetShipCtrl(this);
        HPBar.SetFollowTarget(this.transform);

        newHPBar.gameObject.SetActive(true);
    }
}
