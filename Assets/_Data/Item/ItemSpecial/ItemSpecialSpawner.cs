using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpecialSpawner : Spawner
{
    private static ItemSpecialSpawner instance;
    public static ItemSpecialSpawner Instance => instance;

    [SerializeField] protected float gameDropRate = 1;

    protected override void Awake()
    {
        base.Awake();
        if (ItemSpecialSpawner.instance != null) Debug.LogError("Only 1 ItemSpecialSpawner allow to exist");
        ItemSpecialSpawner.instance = this;
    }

    public virtual List<ItemSpecialDropRate> Drop(List<ItemSpecialDropRate> dropList, Vector3 pos, Quaternion rot)
    {
        List<ItemSpecialDropRate> dropItems = new List<ItemSpecialDropRate>();

        if (dropList.Count < 1) return dropItems;

        dropItems = this.DropItems(dropList);
        foreach (ItemSpecialDropRate itemSpecialDropRate in dropItems)
        {
            ItemSpecialCode itemCode = itemSpecialDropRate.itemProfile.itemCode;
            Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
            if (itemDrop == null) continue;
            itemDrop.gameObject.SetActive(true);
        }
        Debug.LogWarning("Da drop item special");

        return dropItems;
    }

    protected virtual List<ItemSpecialDropRate> DropItems(List<ItemSpecialDropRate> items)
    {
        List<ItemSpecialDropRate> droppedItems = new List<ItemSpecialDropRate>();

        float rate, itemRate;
        int itemDropMore;
        foreach (ItemSpecialDropRate item in items)
        {
            rate = Random.Range(0, 1f);
            itemRate = item.dropRate / 100000f * this.GameDropRate();
            itemDropMore = Mathf.FloorToInt(itemRate);
            if (itemDropMore > 0)
            {
                itemRate -= itemDropMore;
                for (int i = 0; i < itemDropMore; i++)
                {
                    droppedItems.Add(item);
                }
            }

            //Debug.Log("=====================");
            //Debug.Log("item: " + item.itemSO.itemName);
            //Debug.Log("rate: " + itemRate + "/" + rate);
            //Debug.Log("itemRate: " + itemRate);
            //Debug.Log("itemDropMore: " + itemDropMore);

            if (rate <= itemRate)
            {
                //Debug.Log("DROPED");
                droppedItems.Add(item);
            }
        }

        return droppedItems;
    }

    protected virtual float GameDropRate()
    {
        float dropRateFromItems = 0f;

        return this.gameDropRate + dropRateFromItems;
    }

    
}
