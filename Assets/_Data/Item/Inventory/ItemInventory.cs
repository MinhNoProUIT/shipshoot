using System;

[Serializable]
public class ItemInventory
{
    public ItemProfileSO itemProfile; // Thông tin về item (SO = Scriptable Object)
    public int itemCount = 0; // Số lượng item
    public int upgradeLevel = 0; // Cấp độ nâng cấp của item (nếu có)

    public virtual ItemInventory Clone()
    {
        return new ItemInventory
        {
            itemProfile = this.itemProfile,
            itemCount = this.itemCount,
            upgradeLevel = this.upgradeLevel
        };
    }
}
