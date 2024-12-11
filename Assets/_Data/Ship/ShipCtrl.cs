using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCtrl : AbilityObjectCtrl
{
    [Header("Ship")]
    [SerializeField] protected Inventory inventory;
    public Inventory Inventory => inventory;
    //[SerializeField] protected Slider slider;
    [SerializeField] protected ShipProfileSO shipProfileSO;
    public ShipProfileSO ShipProfileSO => shipProfileSO;

    protected override string GetObjectTypeString()
    {
        return ObjectType.Ship.ToString();
    }

    //protected virtual void Update()
    //{
    //    if (slider == null) return;
    //    slider.transform.rotation = Quaternion.Euler(0, 0, 0);
    //}
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventory();
        //this.LoadSlider();
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
