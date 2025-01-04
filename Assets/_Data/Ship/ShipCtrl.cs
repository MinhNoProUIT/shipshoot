using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCtrl : BaseMonoBehaviour
{
    [Header("Ship")]
    [SerializeField] protected Transform model;
    public Transform Model => model;

    [SerializeField] protected ObjShooting objShooting;
    public ObjShooting ObjShooting => objShooting;
    [SerializeField] protected ShipBoundaryScreen objMove;
    public ShipBoundaryScreen ObjMove => objMove;

    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;

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
        this.objShooting = GetComponentInChildren<ObjShooting>();
        Debug.LogWarning(transform.name + ": LoadObjShooting", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

     protected virtual void LoadDameReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.GetComponentInChildren<DamageReceiver>();
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
}
