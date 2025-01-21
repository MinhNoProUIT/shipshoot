using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListItem", menuName = "SO/ListItemDrop")]
public class ListItemSO : ScriptableObject
{
    public List<ItemDropRate> itemDropRates;
}