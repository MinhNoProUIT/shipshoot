using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ItemSpecialLooter : BaseMonoBehaviour
{
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Rigidbody _rigidbody;

    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrigger();
        this.LoadRigidbody();
        LoadShipCtrl();

    }

    protected virtual void LoadShipCtrl(){
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.parent.GetComponent<ShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadShipCtrl", gameObject);
    }

    protected virtual void LoadTrigger()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        this._collider.radius = 0.3f;
        Debug.LogWarning(transform.name + " LoadTrigger", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        this._rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + " LoadRigidbody", gameObject);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

        ItemSpecialPickupable itemSpecialPickupable = collider.GetComponent<ItemSpecialPickupable>();
        if (itemSpecialPickupable == null) return;

        itemSpecialPickupable.Picked();
        Debug.Log(transform.name + ": " +  itemSpecialPickupable.transform.parent.name);
        if(itemSpecialPickupable.transform.parent.name == "IncreaseMovementSpeed") shipCtrl.ObjMove.ActivateSpecialItem();
        if(itemSpecialPickupable.transform.parent.name == "IncreaseFireRate") shipCtrl.ObjShooting.ActivateSpecialItem();
        if(itemSpecialPickupable.transform.parent.name == "MultiBullet") shipCtrl.ObjShooting.ActivateMultiBullet();
        if(itemSpecialPickupable.transform.parent.name == "Heal") shipCtrl.DamageReceiver.Heal(0.25f);
        
    }
}
