using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBoundaryScreen : ObjMoveFoward
{
    [SerializeField] protected float leftBoundary;
    [SerializeField] protected float rightBoundary;
    [SerializeField] protected float topBoundary;
    [SerializeField] protected float bottomBoundary;
    protected override void Start()
    {
        base.Start();
        GetBoundrary();
    }


    protected virtual void GetBoundrary()
    {
        leftBoundary = InputManager.Instance.LeftBoundary;
        rightBoundary = InputManager.Instance.RightBoundary;
        topBoundary = InputManager.Instance.TopBoundary;
        bottomBoundary = InputManager.Instance.BottomBoundary;

        leftBoundary += 1;
        rightBoundary -= 1;
        topBoundary -= 1;
        bottomBoundary += 1;

    }

    protected void ClampPositionWithinBoundaries()
    {
        Vector3 clampedPosition = transform.parent.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomBoundary, topBoundary);
        transform.parent.position = clampedPosition;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ClampPositionWithinBoundaries();
    }
}
