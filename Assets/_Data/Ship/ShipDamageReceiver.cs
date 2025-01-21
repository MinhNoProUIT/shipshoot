using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamageReceiver : DamageReceiver
{

    [SerializeField] LoserManager loserManager;
    
    protected override void OnDead()
    {
        this.shipCtrl.Inventory.GetQuantityByInventory();
        this.countdownTimer.GetCountdownTimerCurrent();
        if(loserManager == null) return;
        loserManager.gameObject.SetActive(true);
    }

    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;

    [SerializeField] protected CountdownTimer countdownTimer;

    protected override void Start()
    {
        base.Start();
        SetHPMax();

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShipCtrl();
        LoadLoserManager();
        LoadCountdownTimer();
    }

    protected virtual void LoadCountdownTimer(){
        if(this.countdownTimer!=null) return;
        this.countdownTimer = GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>();
        Debug.LogWarning(transform.name + ": LoadCountdownTimer", gameObject);

    }

    protected virtual void LoadLoserManager(){
        if(this.loserManager!=null) return;
        this.loserManager = GameObject.Find("Loser").GetComponent<LoserManager>();
        Debug.LogWarning(transform.name + ": LoadLoserManager", gameObject);

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
