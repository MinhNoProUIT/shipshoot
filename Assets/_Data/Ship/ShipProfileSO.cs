using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShipProfileSO", menuName = "SO/ShipProfile")]

public class ShipProfileSO : ScriptableObject
{
    public IdShip ShipId = IdShip.NoId;
    public string ShipName;
    public string Description;

    public Sprite sprite;
    public int dameMax;
    public int hpMax;
    public int speedMovement;
    public int levelUnlock;
    public int coins;
    public int diamonds;
}

public enum IdShip
{
    NoId = 0,

    SH001 = 1,

    SH002 = 2,
    SH003 = 3,

    SH004 = 4,
    SH005 = 5,
    SH006 = 6,
    SH007 = 7,
    SH008 = 8,
    SH009 = 9,
    SH010 = 10,
}
