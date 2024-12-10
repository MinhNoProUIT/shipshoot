using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : BaseMonoBehaviour
{
    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;

    [SerializeField] protected SpawnPoints spawnPoints;
    public SpawnPoints SpawnPoints => spawnPoints;
    [SerializeField] protected SpawnerRandom spawnerRandom;
    public SpawnerRandom SpawnerRandom => spawnerRandom;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadSpawnPoints();
        this.LoadSpawnerRandom();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<Spawner>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints != null) return;
        this.spawnPoints = GameObject.Find("SceneSpawnPoints").GetComponent< SpawnPoints>();
        Debug.Log(transform.name + ": LoadSpawnPoints", gameObject);
    }

    protected virtual void LoadSpawnerRandom()
    {
        if (this.spawnerRandom != null) return;
        this.spawnerRandom = GetComponent<SpawnerRandom>();
        Debug.Log(transform.name + ": LoadSpawnerRandom", gameObject);
    }
}
