using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShootByMouse : ObjShooting
{
    [SerializeField] private float[] shootAngles = { 0f, -15f, 15f };

    [SerializeField] protected float speedGun = 0.01f;
    public float SpeedGun => speedGun;
    protected float originalSpeedGun; // Lưu giá trị tốc độ ban đầu
    protected Coroutine specialItemCoroutine;

    [SerializeField] private bool isMultiBulletActive = false; // Trạng thái MultiBullet
    [SerializeField] private float multiBulletDuration = 10f; // Thời gian hiệu lực của MultiBullet
    private Coroutine multiBulletCoroutine;


    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;

    protected override void Start()
    {
        base.Start();
        SetSpeedGun();

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
            originalSpeedGun = speedGun; // Chỉ lưu một lần
        }

        // Reset hiệu ứng đặc biệt
        specialItemCoroutine = StartCoroutine(SpecialItemEffectCoroutine());
    }

// Coroutine quản lý hiệu ứng đặc biệt
    protected IEnumerator SpecialItemEffectCoroutine()
    {
        // Đặt tốc độ tăng thêm 10% so với tốc độ gốc
        speedGun = originalSpeedGun * 1.1f;
        Debug.Log("Speed Movement current: " + speedGun);

        // Chờ 5 giây
        yield return new WaitForSeconds(10f);

        // Khôi phục tốc độ ban đầu
        speedGun = originalSpeedGun;
        Debug.Log("Speed Movement reset to: " + speedGun);

        // Xóa trạng thái Coroutine
        specialItemCoroutine = null;
    }

    protected virtual void SetSpeedGun(){
        this.speedGun = shipCtrl.ShipProfileSO.speedGun;
        Debug.Log("Speed Movement current: " + this.speedGun);
    }

    protected virtual float GetSpeedMovement(){
        return this.speedGun;
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

    protected override bool IsShooting()
    {
        /* if (InputManager.Instance.OnFiring != 1) this.isShooting = false;
        else this.isShooting = true;
        return this.isShooting; */
        this.isShooting = true;
        return this.isShooting;
    }

    public void ActivateMultiBullet()
    {
        if (multiBulletCoroutine != null)
        {
            StopCoroutine(multiBulletCoroutine);
        }
        multiBulletCoroutine = StartCoroutine(MultiBulletEffectCoroutine());
    }

    private IEnumerator MultiBulletEffectCoroutine()
    {
        isMultiBulletActive = true; // Kích hoạt MultiBullet
        Debug.Log("MultiBullet Activated!");

        yield return new WaitForSeconds(multiBulletDuration); // Chờ 10 giây

        isMultiBulletActive = false; // Tắt MultiBullet
        Debug.Log("MultiBullet Deactivated!");

        multiBulletCoroutine = null;
    }

    protected override void Shooting()
    {
        this.shootTimer += Time.fixedDeltaTime;

        if (!this.isShooting) return;
        if (this.shootTimer < this.shootDelay * (1 - this.speedGun / 100)) return;
        this.shootTimer = 0;

        Vector3 spawnPos = transform.position;
        Quaternion baseRotation = transform.parent.rotation;

        if (isMultiBulletActive)
        {
            // Bắn 3 tia khi MultiBullet được kích hoạt
            foreach (float angle in shootAngles)
            {
                Quaternion bulletRotation = baseRotation * Quaternion.Euler(0, 0, angle); // Tạo góc quay mới
                Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletBlue, spawnPos, bulletRotation);
                if (newBullet == null) continue;

                newBullet.gameObject.SetActive(true);
                BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
                bulletCtrl.SetShotter(transform.parent);
            }
        }
        else
        {
            // Bắn 1 tia bình thường
            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletBlue, spawnPos, baseRotation);
            if (newBullet == null) return;

            newBullet.gameObject.SetActive(true);
            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShotter(transform.parent);
        }
    }



}
