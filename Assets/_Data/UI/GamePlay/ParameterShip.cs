using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParameterShip : BaseMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txtHp, txtDamage, txtSpeedMove, txtSpeedGun;
    [SerializeField] protected ShipCtrl shipCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHP();
        LoadDamage();
        LoadSpeedMove();
        LoadSpeedGun();
        LoadShipCtrl();
    }

    protected virtual void LoadHP(){
        if (this.txtHp != null) return;
        this.txtHp = transform.Find("HP").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadHP", gameObject);
    }
    protected virtual void LoadDamage(){
        if (this.txtDamage != null) return;
        this.txtDamage = transform.Find("Damage").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadDamage", gameObject);
    }
    protected virtual void LoadSpeedMove(){
        if (this.txtSpeedMove != null) return;
        this.txtSpeedMove = transform.Find("SpeedMove").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadSpeedMove", gameObject);
    }
    protected virtual void LoadSpeedGun(){
        if (this.txtSpeedGun != null) return;
        this.txtSpeedGun = transform.Find("SpeedGun").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadSpeedGun", gameObject);
    }

    protected virtual void LoadShipCtrl(){
        if (this.shipCtrl != null) return;
        this.shipCtrl = GameObject.Find("Ship").GetComponent<ShipCtrl>();
        Debug.Log(transform.name + ": LoadShipCtrl", gameObject);
    }

    protected virtual void Update(){
        if(shipCtrl == null) return;

        this.txtHp.text = $"{this.shipCtrl.DamageReceiver.HP}/{this.shipCtrl.DamageReceiver.HPMax}";
        this.txtDamage.text = this.shipCtrl.ShipProfileSO.dameMax.ToString();
        this.txtSpeedGun.text = this.shipCtrl.ObjShooting.SpeedGun.ToString();
        this.txtSpeedMove.text = this.shipCtrl.ObjMove.SpeedMovement.ToString();
    }
}
