using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MeteoriteSO", menuName = "SO/MeteoriteSO")]
public class MeteoriteSO : ScriptableObject
{
    public string meteoriteName;
    public Sprite imageMeteorite;
    public ListItemSO listItem;
    public ListItemSpecialSO listItemSpecial;
    public int HP;
    public int speedRun;
}