using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BossSO", menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;
    public BulletSO bullet;
    public ListItemSpecialSO listItemSpecial;
    public int HP;
    public int damage;
    public int speedRun;
    public int speedGun;
    public List<EnemySO> listEnemySpawn;

}