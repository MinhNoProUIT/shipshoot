using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipSpawner : Spawner
{
    private static SpaceshipSpawner instance;
    public static SpaceshipSpawner Instance => instance;

    public static string spaceship = "SH001";

    protected override void Awake()
    {
        base.Awake();
        if (SpaceshipSpawner.instance != null) Debug.LogError("Only 1 SpaceshipSpawner allow to exist");
        SpaceshipSpawner.instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //LoadSpaceship();
    }

    protected override void Start()
    {
        base.Start();
        LoadSpaceship();
    }

    protected virtual void LoadSpaceship()
    {
        Debug.LogWarning("Da load Spaceship");
        DatabaseManager.Instance.GetOwnedSpaceships(DatabaseManager.Instance.GetUserId(), (ownedShips) =>
        {
            if (ownedShips != null)
            {
                Debug.Log("Owned ship not null");
                Debug.Log($"Total ships: {ownedShips.Count}");

                foreach (var ship in ownedShips)
                {
                    Debug.Log($"Phi thuyền: {ship.Key}, Trạng thái: {ship.Value}");
                    if (ship.Value == 1) // Chỉ spawn phi thuyền đã sở hữu
                    {
                        Vector3 spawnPosition = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), 0); // Vị trí ngẫu nhiên
                        Quaternion spawnRotation = Quaternion.identity;

                        Transform spawnedShip = Spawn("SH001", spawnPosition, spawnRotation);
                        if (spawnedShip != null)
                        {
                            Debug.Log($"Spawned spaceship: {ship.Key}");
                        }
                        else
                        {
                            Debug.LogError($"Failed to spawn spaceship: {ship.Key}");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Không thể lấy danh sách phi thuyền.");
            }
        });
    }


    public override Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);

        RectTransform rectTransform = newPrefab.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Xử lý RectTransform cho Canvas
            rectTransform.SetParent(this.holder, false);
            rectTransform.anchoredPosition = spawnPos;
            rectTransform.localRotation = rotation;
            rectTransform.localScale = Vector3.one;
        }
        else
        {
            // Xử lý Transform thông thường
            newPrefab.SetParent(this.holder);
            newPrefab.SetPositionAndRotation(spawnPos, rotation);
        }

        this.spawnedCount++;
        return newPrefab;
    }


}
