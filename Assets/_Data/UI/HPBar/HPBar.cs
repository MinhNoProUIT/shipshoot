using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : BaseMonoBehaviour
{
    [Header("HP Bar")]

    [SerializeField] protected ShootableObjectCtrl shootableObjectCtrl;
    [SerializeField] protected ShipSliderHP shipSliderHp;
    [SerializeField] protected FollowTarget followTarget;
    [SerializeField] protected Spawner spawner;

    [SerializeField] protected int maxHP = 0;
    [SerializeField] protected int currentHP = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSliderHP();
        this.LoadFollowTarget();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent.parent.GetComponent<Spawner>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }


    protected virtual void LoadFollowTarget()
    {
        if (this.followTarget != null) return;
        this.followTarget = transform.GetComponent<FollowTarget>();
        Debug.Log(transform.name + ": LoadFollowTarget", gameObject);
    }

    protected virtual void LoadSliderHP()
    {
        if (this.shipSliderHp != null) return;
        this.shipSliderHp = transform.GetComponentInChildren<ShipSliderHP>();
        Debug.Log(transform.name + ": LoadSliderHP", gameObject);
    }

    protected virtual void Update()
    {
        if (shootableObjectCtrl == null) return;
        this.maxHP = shootableObjectCtrl.DamageReceiver.HPMax;
        this.currentHP = shootableObjectCtrl.DamageReceiver.HP;

        bool isDead = shootableObjectCtrl.DamageReceiver.IsDead();
        if (isDead) this.spawner.Despawn(transform);

        shipSliderHp.SetCurrentHp(this.currentHP);
        shipSliderHp.SetMaxHp(this.maxHP);

    }

    public virtual void SetFollowTarget(Transform target)
    {
        followTarget.SetTarget(target);
    }

    public virtual void SetObjectCtrl(ShootableObjectCtrl shootableObjectCtrl)
    {
        this.shootableObjectCtrl = shootableObjectCtrl;
    }

}