using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjectDamReceiver : DamageReceiver
{
    [Header("Shootable Object")]
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected override void OnDead()
    {
        this.OnDeadFX();
        this.OnDeadDrop();
        this.enemyCtrl.Despawn.DespawnObject();

    }

    protected virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        //Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(this.enemyCtrl.ShootableObject.dropList, dropPos, Quaternion.identity);
        ItemSpecialSpawner.Instance.Drop(this.enemyCtrl.ShootableSpecialObjectSO.itemDropRates, dropPos, Quaternion.identity);
        //ItemSpecialSpawner.Instance.Drop(this.shootablObjectCtrl.ShootableObject.dropList, dropPos, Quaternion.identity);

        Debug.Log("Da drop");
    }

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smoke1;
    }

    public override void Reborn()
    {
        this.hpMax = this.enemyCtrl.ShootableObject.hpMax;
        base.Reborn();
    }
}
