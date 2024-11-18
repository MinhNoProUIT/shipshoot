using System;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        EnemySpawner.instance = this;
    }

    public override Transform Spawn(Transform prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newEnemy = base.Spawn(prefabName, spawnPos, rotation);
        this.CreateHPBarByNewEnemy(newEnemy);
        return newEnemy;
    }

    public virtual void CreateHPBarByNewEnemy(Transform newEnemy)
    {
        ShootableObjectCtrl shootableObject = newEnemy.GetComponent<ShootableObjectCtrl>();
        if (shootableObject == null) return;
        
        Transform newHPBar = HPBarSpawner.Instance.Spawn(HPBarSpawner.HPBar, newEnemy.position, Quaternion.identity);

        HPBar HPBar = newHPBar.GetComponent<HPBar>();
        if (HPBar == null) return;

        HPBar.SetObjectCtrl(shootableObject);
        HPBar.SetFollowTarget(newEnemy);

        newHPBar.gameObject.SetActive(true);
    }
}
