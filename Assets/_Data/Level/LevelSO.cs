using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSO", menuName = "SO/Level")]

public class LevelSO : ScriptableObject
{
    public int level;
    public int killPerLevel;
}
