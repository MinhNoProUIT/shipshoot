using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDameReceiver : DamageReceiver
{

    [Header("Boss Receiver")]
    [SerializeField] protected BossCtrl bossCtrl;
    [SerializeField] protected WinnerManager winnerManager;
    [SerializeField] protected ShipCtrl shipCtrl;
    public ShipCtrl ShipCtrl => shipCtrl;

    [SerializeField] protected CountdownTimer countdownTimer;

    [SerializeField] protected bool isSpawnItemSpecial = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadWinnerManager();
        LoadCountdownTimer();
        LoadShipCtrl();

    }

    protected virtual void LoadShipCtrl(){
        if (this.shipCtrl != null) return;
        this.shipCtrl = GameObject.Find("Ship").GetComponent<ShipCtrl>();
        Debug.LogWarning(transform.name + ": LoadShipCtrl", gameObject);
    }

    protected virtual void LoadCountdownTimer(){
        if(this.countdownTimer!=null) return;
        this.countdownTimer = GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>();
        Debug.LogWarning(transform.name + ": LoadCountdownTimer", gameObject);

    }

    protected virtual void LoadWinnerManager(){
        if(this.winnerManager!=null) return;
        this.winnerManager = GameObject.Find("Winner").GetComponent<WinnerManager>();
        Debug.LogWarning(transform.name + ": LoadWinnerManager", gameObject);

    }

    protected virtual void LoadCtrl()
    {
        if (this.bossCtrl != null) return;
        this.bossCtrl = transform.parent.GetComponent<BossCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }
    protected override void OnDead()
    {
        this.OnDeadFX();
        this.shipCtrl.Inventory.GetQuantityByInventory();
        this.countdownTimer.GetCountdownTimerCurrent();
        ShowUIWinner();
        //Destroy(transform.parent);
    }

    public override void Deduct(int deduct)
    {
        base.Deduct(deduct);

        if(!isSpawnItemSpecial) return;

        if(hp<= 0.5f*hpMax) {
            Vector3 dropPos = transform.position;
            ItemSpecialSpawner.Instance.Drop(this.bossCtrl.BossSO.listItemSpecial.itemDropRates, dropPos, Quaternion.identity);
            isSpawnItemSpecial = true;
        }
    }

    protected virtual void ShowUIWinner(){
        if(winnerManager == null) return;
        winnerManager.gameObject.SetActive(true);
    }

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smoke1;
    }

    public override void Reborn()
    {
        //this.hpMax = this.bossCtrl.BossSO.HP;
        this.hpMax = this.bossCtrl.BossSO.HP;
        base.Reborn();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.hpMax = this.bossCtrl.BossSO.HP;

    }


}
