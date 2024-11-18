using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLookAtMouse: ObjLookAtTarget
{
    protected override void FixedUpdate()
    {
        this.GetMousePosition();
        base.FixedUpdate();
    }

    protected virtual void GetMousePosition()
    {
        this.targetPosition = InputManager.Instance.MouseWorldPos;
        this.targetPosition.z = 0;
        //this.targetPosition.y = 0;
        //this.targetPosition.x = 0;
    }

}
