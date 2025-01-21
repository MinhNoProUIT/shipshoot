using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemySO", menuName = "SO/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite imageEnemy;
    public BulletSO bullet;
    public ListItemSO listItem;
    public ListItemSpecialSO listItemSpecial;
    public int HP;
    public int damage;
    public int speedRun;
    public int speedGun;
}