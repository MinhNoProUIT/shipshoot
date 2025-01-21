using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListItemSpecial", menuName = "SO/ListItemSpecialDrop")]
public class ListItemSpecialSO : ScriptableObject
{
    public List<ItemSpecialDropRate> itemDropRates;
}