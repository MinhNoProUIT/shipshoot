using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShootByMouse : ObjShooting
{
    [SerializeField] private float[] shootAngles = { 0f, -15f, 15f };

    protected override bool IsShooting()
    {
        if (InputManager.Instance.OnFiring != 1) this.isShooting = false;
        else this.isShooting = true;
        return this.isShooting;
    }

    protected override void Shooting()
    {
        this.shootTimer += Time.fixedDeltaTime;

        if (!this.isShooting) return;
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0;

        Vector3 spawnPos = transform.position;
        Quaternion baseRotation = transform.parent.rotation;

        // Tạo đạn ở 3 góc: thẳng, lệch trái 30 độ, lệch phải 30 độ
        //float[] angles = { 0f, -30f, 30f };

        foreach (float angle in shootAngles)
        {
            Quaternion bulletRotation = baseRotation * Quaternion.Euler(0, 0, angle); // Tạo góc quay mới
            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, spawnPos, bulletRotation);
            if (newBullet == null) continue;

            newBullet.gameObject.SetActive(true);
            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShotter(transform.parent);
        }
    }

}
