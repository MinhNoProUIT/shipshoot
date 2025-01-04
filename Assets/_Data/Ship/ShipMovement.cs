using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;  // Tốc độ di chuyển
    [SerializeField] private float minDistance = 0.1f;  // Khoảng cách tối thiểu để ngừng di chuyển

    private Vector3 targetPosition;

    void Update()
    {
        GetMousePosition();   // Lấy vị trí chuột
        MoveToTarget();       // Di chuyển tới vị trí mục tiêu
    }

    private void GetMousePosition()
    {
        // Lấy vị trí chuột trong không gian thế giới
        targetPosition = InputManager.Instance.MouseWorldPos;
    }

    private void MoveToTarget()
    {
        // Tính khoảng cách từ đối tượng đến mục tiêu
        float distance = Vector3.Distance(transform.parent.position, targetPosition);

      
            // Di chuyển đối tượng theo hướng mục tiêu
            transform.position = Vector3.Lerp(transform.parent.position, targetPosition, speed * Time.deltaTime);
        
    }
}
