using System.Collections.Generic;
using UnityEngine;

public class Inventory : BaseMonoBehaviour
{
    [SerializeField] protected int maxSlot = 7; // Số lượng slot tối đa trong inventory
    [SerializeField] protected List<ItemInventory> items; // Danh sách các item trong inventory

    public List<ItemInventory> Items => items; // Truy cập danh sách item từ bên ngoài

    protected override void Start()
    {
        base.Start();
        // Khởi tạo inventory (nếu cần)
    }

    /// <summary>
    /// Thêm item vào inventory. Cộng dồn nếu đã tồn tại, hoặc thêm mới nếu chưa tồn tại.
    /// </summary>
    public virtual bool AddItem(ItemInventory itemInventory)
    {
        ItemProfileSO itemProfile = itemInventory.itemProfile;
        ItemCode itemCode = itemProfile.itemCode;

        // Tìm item hiện có trong inventory
        ItemInventory existingItem = GetItemByCode(itemCode);

        if (existingItem != null)
        {
            // Cộng dồn số lượng
            existingItem.itemCount += itemInventory.itemCount;
        }
        else
        {
            if (IsInventoryFull()) return false;

            // Thêm item mới vào inventory
            ItemInventory newItem = itemInventory.Clone();
            this.items.Add(newItem);
        }

        return true;
    }

    /// <summary>
    /// Kiểm tra inventory có đầy không.
    /// </summary>
    protected virtual bool IsInventoryFull()
    {
        return this.items.Count >= this.maxSlot;
    }

    /// <summary>
    /// Lấy item từ inventory dựa trên mã code.
    /// </summary>
    protected virtual ItemInventory GetItemByCode(ItemCode itemCode)
    {
        foreach (ItemInventory item in this.items)
        {
            if (item.itemProfile.itemCode == itemCode)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Kiểm tra tổng số lượng item có đủ yêu cầu không.
    /// </summary>
    public virtual bool ItemCheck(ItemCode itemCode, int numberCheck)
    {
        return ItemTotalCount(itemCode) >= numberCheck;
    }

    /// <summary>
    /// Lấy tổng số lượng của một loại item trong inventory.
    /// </summary>
    public virtual int ItemTotalCount(ItemCode itemCode)
    {
        int totalCount = 0;
        foreach (ItemInventory item in this.items)
        {
            if (item.itemProfile.itemCode == itemCode)
            {
                totalCount += item.itemCount;
            }
        }
        return totalCount;
    }

    /// <summary>
    /// Trừ số lượng item trong inventory.
    /// </summary>
    public virtual void DeductItem(ItemCode itemCode, int deductCount)
    {
        for (int i = this.items.Count - 1; i >= 0; i--)
        {
            ItemInventory item = this.items[i];
            if (item.itemProfile.itemCode != itemCode) continue;

            if (deductCount > item.itemCount)
            {
                deductCount -= item.itemCount;
                item.itemCount = 0;
            }
            else
            {
                item.itemCount -= deductCount;
                deductCount = 0;
            }

            if (item.itemCount <= 0)
            {
                this.items.RemoveAt(i); // Loại bỏ item nếu số lượng bằng 0
            }

            if (deductCount <= 0) break;
        }
    }
}
