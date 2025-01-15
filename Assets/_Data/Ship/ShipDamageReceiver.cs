using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        //Nothing for
    }

    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;

    protected override void Start()
    {
        base.Start();
        SetHPMax();

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

    protected virtual void SetHPMax(){
        this.hp = shipCtrl.ShipProfileSO.hpMax;
        this.hpMax = shipCtrl.ShipProfileSO.hpMax;
    }

    public virtual void Heal(float percent)
    {
        if (isDead) return;

        int healAmount = Mathf.CeilToInt(hpMax * percent); // Tính lượng máu hồi (làm tròn lên)
        hp += healAmount;

        if (hp > hpMax) hp = hpMax; // Giới hạn không vượt quá hpMax

        Debug.Log($"Healed {healAmount} HP. Current HP: {hp}/{hpMax}");
    }
    
}
