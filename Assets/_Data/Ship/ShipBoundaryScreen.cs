using System.Collections;
using System.Collections.Generic;
using PlayFab.EconomyModels;
using UnityEngine;

public class ShipBoundaryScreen : BaseMonoBehaviour
{
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected float speed = 0.001f;
    [SerializeField] protected float speedMovement = 0.01f;
    public float SpeedMovement => speedMovement;
    protected float originalSpeedMovement; // Lưu giá trị tốc độ ban đầu
    protected Coroutine specialItemCoroutine;
    [SerializeField] protected float distance = 1f;
    [SerializeField] protected float minDistance = 1f;

    [Header("Boundary Screen")]
    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;
    [SerializeField] protected float leftBoundary;
    [SerializeField] protected float rightBoundary;
    [SerializeField] protected float topBoundary;
    [SerializeField] protected float bottomBoundary;
    
    protected override void Start()
    {
        base.Start();
        GetBoundrary();
        SetSpeedMovement();
    }

    public virtual void ActivateSpecialItem()
    {
        // Nếu đã có hiệu ứng đặc biệt, hủy và áp dụng lại để làm mới thời gian
        if (specialItemCoroutine != null)
        {
            StopCoroutine(specialItemCoroutine);
        }

        // Lưu tốc độ gốc nếu đây là lần đầu nhặt item
        if (specialItemCoroutine == null)
        {
            originalSpeedMovement = speedMovement; // Chỉ lưu một lần
        }

        // Reset hiệu ứng đặc biệt
        specialItemCoroutine = StartCoroutine(SpecialItemEffectCoroutine());
    }

// Coroutine quản lý hiệu ứng đặc biệt
    protected IEnumerator SpecialItemEffectCoroutine()
    {
        // Đặt tốc độ tăng thêm 10% so với tốc độ gốc
        speedMovement = originalSpeedMovement * 1.1f;
        Debug.Log("Speed Movement current: " + speedMovement);

        // Chờ 5 giây
        yield return new WaitForSeconds(5f);

        // Khôi phục tốc độ ban đầu
        speedMovement = originalSpeedMovement;
        Debug.Log("Speed Movement reset to: " + speedMovement);

        // Xóa trạng thái Coroutine
        specialItemCoroutine = null;
    }

    protected virtual void SetSpeedMovement(){
        this.speedMovement = shipCtrl.ShipProfileSO.speedMovement;
        Debug.Log("Speed Movement current: " + this.speedMovement);
    }

    protected virtual float GetSpeedMovement(){
        return this.speedMovement;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShipCtrl();
    }

    protected virtual void LoadShipCtrl(){
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.parent.GetComponent<ShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadShipCtrl", gameObject);
    }


    protected virtual void GetBoundrary()
    {
        leftBoundary = InputManager.Instance.LeftBoundary;
        rightBoundary = InputManager.Instance.RightBoundary;
        topBoundary = InputManager.Instance.TopBoundary;
        bottomBoundary = InputManager.Instance.BottomBoundary;

        leftBoundary += 0.5f;
        rightBoundary -= 0.5f;
        topBoundary -= 0.5f;
        bottomBoundary += 0.5f; 

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

        Vector3 newPos = Vector3.Lerp(transform.parent.position, targetPosition, this.speed * this.speedMovement);
        transform.parent.position = newPos;
    }

    public virtual void GetMousePosition(){
        this.targetPosition = InputManager.Instance.MouseWorldPos;
        this.targetPosition.z = 0;
    }
}
