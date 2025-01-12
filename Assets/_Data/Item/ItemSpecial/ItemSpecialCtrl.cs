using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpecialCtrl : BaseMonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected ItemSpecialDespawn itemSpecialDespawn;
    public ItemSpecialDespawn ItemSpecialDespawn => itemSpecialDespawn;

    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemSpecialDespawn();
        this.LoadItemInventory();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetItem();
    }

    protected virtual void LoadItemSpecialDespawn()
    {
        if (this.itemSpecialDespawn != null) return;
        this.itemSpecialDespawn = transform.GetComponentInChildren<ItemSpecialDespawn>();
        Debug.Log(transform.name + ": LoadItemDespawn", gameObject);
    }

    public virtual void SetItemInventory(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory.Clone();

        //this.itemInventory = new ItemInventory();
        //this.itemInventory.itemProfile = itemInventory.itemProfile;
        //this.itemInventory.itemCount = itemInventory.itemCount;
        //this.itemInventory.upgradeLevel = itemInventory.upgradeLevel;
    }

    protected virtual void LoadItemInventory()
    {
        if (this.itemInventory.itemProfile != null) return;
        ItemCode itemCode = ItemCodeParser.FromString(transform.name);
        ItemProfileSO itemProfile = ItemProfileSO.FindByItemCode(itemCode);
        this.itemInventory.itemProfile = itemProfile;
        this.ResetItem();
        Debug.Log(transform.name + ": LoadItemInventory", gameObject);
    }

    protected virtual void ResetItem()
    {
        this.itemInventory.itemCount = 1;
        this.itemInventory.upgradeLevel = 0;
    }
}
