using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShootableSpecialObjectSO", menuName = "SO/ShootableSpecialObject")]
public class ShootableSpecialObjectSO : ScriptableObject
{
    public List<ItemSpecialDropRate> itemDropRates;
}