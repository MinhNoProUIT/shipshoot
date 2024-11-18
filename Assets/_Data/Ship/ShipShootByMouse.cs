using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShootByMouse : ObjShooting
{
    protected override bool IsShooting()
    {
        if (InputManager.Instance.OnFiring != 1) this.isShooting = false;
        else this.isShooting = true;
        return this.isShooting;
    }
}
