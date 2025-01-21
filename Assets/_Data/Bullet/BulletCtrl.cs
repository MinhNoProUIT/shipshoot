using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : BaseMonoBehaviour
{
    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender { get => damageSender; }

    [SerializeField] protected BulletDespawn bulletDespawn;
    public BulletDespawn BulletDespawn { get => bulletDespawn; }

    [SerializeField] protected Transform shooter;
    public Transform Shooter => shooter;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDamageSender();
        this.LoadBulletDespawn();
    }

    protected virtual void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = transform.GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }

    protected virtual void LoadBulletDespawn()
    {
        if (this.bulletDespawn != null) return;
        this.bulletDespawn = transform.GetComponentInChildren<BulletDespawn>();
        Debug.Log(transform.name + ": LoadBulletDespawn", gameObject);
    }

    public virtual void SetShotter(Transform shooter)
    {
        this.shooter = shooter;
        Debug.LogWarning(shooter.transform.name);
        if(shooter.transform.name == "Ship")
        {
            this.damageSender.SetDamage(shooter.GetComponent<ShipCtrl>().ShipProfileSO.dameMax);
        }
        else if(shooter.transform.name == "Boss") {
            this.damageSender.SetDamage(shooter.GetComponent<BossCtrl>().BossSO.damage);

        }
        else{
            this.damageSender.SetDamage(shooter.GetComponent<EnemyCtrl>().EnemySO.damage);
        }
    }

}
