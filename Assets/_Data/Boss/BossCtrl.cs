using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : BaseMonoBehaviour
{
    [Header("Ship")]
    [SerializeField] protected SpriteRenderer model;
    public SpriteRenderer Model => model;

    [SerializeField] protected BossShooting bossShooting;
    public BossShooting BossShooting => bossShooting;
    [SerializeField] protected ObjMoveFoward objMoveFoward;
    public ObjMoveFoward ObjMoveFoward => objMoveFoward;

    [SerializeField] protected DamageReceiver bossDameReceiver;
    public DamageReceiver BossDameReceiver => bossDameReceiver;

    [SerializeField] protected BossSO bossSO;
    public BossSO BossSO => bossSO;

    protected override void LoadComponents()
    {
        this.LoadModel();
        this.LoadObjShooting();
        this.LoadDameReceiver();

        base.LoadComponents();
        this.LoadObjMovement();
        //this.LoadSlider();
    }

    protected virtual void LoadObjMovement()
    {
        if (this.objMoveFoward != null) return;
        this.objMoveFoward = GetComponentInChildren<ObjMoveFoward>();
        Debug.LogWarning(transform.name + ": LoadObjMovement", gameObject);
    }

    protected virtual void LoadObjShooting()
    {
        if (this.bossShooting != null) return;
        this.bossShooting = GetComponentInChildren<BossShooting>();
        Debug.LogWarning(transform.name + ": LoadObjShooting", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

     protected virtual void LoadDameReceiver()
    {
        if (this.bossDameReceiver != null) return;
        this.bossDameReceiver = transform.GetComponentInChildren<DamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadDameReceiver", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if(bossSO == null) return;
        this.model.sprite = bossSO.sprite;
        CreateHPBar();
    }

    public virtual void CreateHPBar(){
        //S shootableObject = newEnemy.GetComponent<ShootableObjectCtrl>();
        //if (shootableObject == null) return;
        
        Transform newHPBar = HPBarSpawner.Instance.Spawn(HPBarSpawner.HPBar, transform.position + new Vector3(0,1.2f,0), Quaternion.identity);

        HPBar HPBar = newHPBar.GetComponent<HPBar>();
        if (HPBar == null) return;

        HPBar.SetBossCtrl(this);
        HPBar.SetFollowTarget(this.transform);

        newHPBar.gameObject.SetActive(true);
    }
}
