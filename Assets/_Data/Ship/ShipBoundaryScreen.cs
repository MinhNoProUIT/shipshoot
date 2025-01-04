using System.Collections;
using System.Collections.Generic;
using PlayFab.EconomyModels;
using UnityEngine;

public class ShipBoundaryScreen : BaseMonoBehaviour
{
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected float speed = 0.01f;
    [SerializeField] protected float distance = 1f;
    [SerializeField] protected float minDistance = 1f;

    [Header("Boundary Screen")]
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

    protected virtual void FixedUpdate()
    {
        this.Moving();
        ClampPositionWithinBoundaries();
    }

    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    protected virtual void Moving()
    {
        /* this.distance = Vector3.Distance(transform.position, this.targetPosition);
        if (this.distance < this.minDistance) return; */
        GetMousePosition();

        Vector3 newPos = Vector3.Lerp(transform.parent.position, targetPosition, this.speed);
        transform.parent.position = newPos;
    }

    public virtual void GetMousePosition(){
        this.targetPosition = InputManager.Instance.MouseWorldPos;
        this.targetPosition.z = 0;
    }
}
