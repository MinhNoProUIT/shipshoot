using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ItemSpecialProfileSO: ScriptableObject chứa các thông tin item đặc biệt
[CreateAssetMenu(fileName = "ItemSpecialProfileSO", menuName = "SO/ItemSpecialProfile")]
public class ItemSpecialProfileSO : ScriptableObject
{
    public ItemSpecialCode itemCode;
    public string itemName;
    public Sprite image;
    public int time; // Thời gian hiệu lực của item (giây)
}

public enum ItemSpecialCode
{
    None,
    Shield,
    Heal,
    IncreaseFireRate,
    IncreaseMovementSpeed,
    MultiBullet
}
