using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : ObjShooting
{
    protected override bool IsShooting()
    {
        this.isShooting = true;
        return this.isShooting;
    }

    protected override void Shooting()
    {
        this.shootTimer += Time.fixedDeltaTime;

        if (!this.isShooting) return;
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0;

        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.parent.rotation;
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, spawnPos, rotation);
        if (newBullet == null) return;

        newBullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        if(bulletCtrl==null) return;
        //bulletCtrl.SetShotter(transform.parent);
    }

    // Start is called before the first frame update

}
