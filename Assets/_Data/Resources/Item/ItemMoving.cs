using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoving : BaseMonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float moveSpeedEffect = 5f;

    [SerializeField] protected Vector3 direction = Vector3.down;
    [SerializeField] protected Transform ship; // Tham chiếu đến đối tượng tàu
    [SerializeField] protected float attractionDistance = 2f; // Khoảng cách kích hoạt
    [SerializeField] Vector2 positionItem;
    [SerializeField] float distanceToShip;

    void Update()
    {
        if (ship == null)
        {
            Debug.LogWarning("Ship is not assigned!");
            return;
        }
        positionItem = transform.parent.position;

        // Tính khoảng cách giữa vật phẩm và tàu
        distanceToShip = Vector3.Distance(transform.parent.position, ship.position);

        if (distanceToShip <= attractionDistance)
        {
            // Di chuyển về phía tàu
            Vector3 directionToShip = (ship.position - transform.parent.position).normalized;
            transform.parent.Translate(directionToShip * moveSpeedEffect * Time.deltaTime);
            Debug.Log("Da di chuyen toi tau");
        }
        else
        {
            // Tiếp tục di chuyển theo hướng mặc định
            transform.parent.Translate(this.direction * this.moveSpeed * Time.deltaTime);
        }
    }
}
