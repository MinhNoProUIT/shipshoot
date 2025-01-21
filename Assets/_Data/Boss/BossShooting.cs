using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : ObjShooting
{
    private int bulletsPerRound = 36; // Số viên đạn mỗi vòng (360 độ / 10 độ)
    private float angleStep = 10f;    // Góc giữa các viên đạn
    private float burstInterval = 0.5f; // Thời gian giữa các lần bắn trong một đợt
    private float waveInterval = 5f;   // Thời gian nghỉ giữa các đợt bắn
    private int burstsPerWave = 3;    // Số lần bắn trong một đợt

    private bool isWaveActive = true;

    protected override bool IsShooting()
    {
        return isWaveActive;
    }

    protected override void Shooting()
    {
        if (!IsShooting()) return;
        StartCoroutine(ShootingWave());
    }

    private IEnumerator ShootingWave()
    {
        isWaveActive = false;
        for (int i = 0; i < burstsPerWave; i++)
        {
            FireBullets();
            yield return new WaitForSeconds(burstInterval);
        }

        yield return new WaitForSeconds(waveInterval - (burstsPerWave * burstInterval));
        isWaveActive = true;
    }

    private void FireBullets()
    {
        float angleStart = 0f; // Bắt đầu từ góc 0 độ
        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < bulletsPerRound; i++)
        {
            float bulletAngle = angleStart + (i * angleStep);
            Quaternion rotation = Quaternion.Euler(0f, 0f, bulletAngle);

            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, spawnPosition, rotation);
            if (newBullet == null) continue;

            newBullet.gameObject.SetActive(true);
            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShotter(transform.parent);
        }
    }
}
