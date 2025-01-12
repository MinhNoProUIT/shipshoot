using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircularMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform center;  // Tâm quay
    public float radius = 5f; // Bán kính quay
    public float duration = 2f; // Thời gian hoàn thành một vòng
    public bool clockwise = true; // Hướng quay

    private void Start()
    {
        MoveAroundCenter();
    }

    private void MoveAroundCenter()
    {
        // Tính toán vị trí ban đầu
        Vector3 startPosition = center.position + new Vector3(radius, 0, 0);

        // Di chuyển quanh tâm bằng cách tạo quỹ đạo tròn
        transform.position = startPosition;

        Sequence sequence = DOTween.Sequence();

        // Tạo hoạt ảnh di chuyển theo quỹ đạo
        sequence.Append(
            transform
                .DOLocalPath(
                    CreateCirclePath(),
                    duration,
                    PathType.CatmullRom
                )
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
        );

        // Cập nhật rotation để hướng mũi phi thuyền
        /* sequence.OnUpdate(() =>
        {
            Vector3 direction = (transform.position - center.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - (clockwise ? 90 : -90));
        }); */
    }

    private Vector3[] CreateCirclePath()
    {
        // Tạo danh sách điểm để tạo một vòng tròn
        int resolution = 100;
        Vector3[] path = new Vector3[resolution];
        float angleStep = 360f / resolution;

        for (int i = 0; i < resolution; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            float x = center.position.x + Mathf.Cos(angle) * radius;
            float y = center.position.y + Mathf.Sin(angle) * radius;
            path[i] = new Vector3(x, y, 0);
        }

        return path;
    }
}
