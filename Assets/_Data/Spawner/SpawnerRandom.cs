using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRandom : BaseMonoBehaviour
{
    [Header("Spawner Random")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 9f;
    [SerializeField] protected int enemiesCurrent = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnerCtrl();
    }

    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadSpawnerCtrl", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.JunkSpawning();
    }


    protected virtual void JunkSpawning()
    {
        if (this.RandomReachLimit()) return;
        //if(LevelByKillEnemy.Instance.LevelCurrent == 1) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform ranPoint = this.spawnerCtrl.SpawnPoints.GetRandom();
        Vector3 pos = ranPoint.position;
        Quaternion rot = transform.rotation;

        Transform prefab = this.spawnerCtrl.Spawner.RandomPrefab();
        Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, pos, rot);
        obj.gameObject.SetActive(true);

        enemiesCurrent++;
    }

    protected virtual bool RandomReachLimit()
    {
        return enemiesCurrent > this.randomLimit;
    }

    public virtual void SetRandomLimit(int limit)
    {
        this.randomLimit = limit;
        this.enemiesCurrent = 0;
    }
}