using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShootByDistance : ObjShooting
{
    [Header("Shoot by Distance")]
    [SerializeField] protected float speedGun = 1f;
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float shootDistance = 3f;
    [SerializeField] protected ShootableObjectCtrl shootableObjectCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShootableObjectCtrl();
        LoadSpeedGun();
    }

    protected virtual void LoadShootableObjectCtrl(){
        if (this.shootableObjectCtrl != null) return;
        this.shootableObjectCtrl = transform.parent.GetComponent<ShootableObjectCtrl>();
        Debug.LogWarning(transform.name + ": LoadShootableObjectCtrl", gameObject);
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected override bool IsShooting()
    {
        this.distance = Vector3.Distance(transform.position, this.target.position);
        this.isShooting = this.distance < this.shootDistance;
        return this.isShooting;
    }

    protected override void Start()
    {
        base.Start();
        LoadSpeedGun();
    }

    protected virtual void LoadSpeedGun(){
        if(shootableObjectCtrl == null) return;
        if(shootableObjectCtrl.EnemySO == null) return;
        this.speedGun = shootableObjectCtrl.EnemySO.speedGun;
        this.shootDelay = 1 - this.speedGun/100;
    }

    

}
