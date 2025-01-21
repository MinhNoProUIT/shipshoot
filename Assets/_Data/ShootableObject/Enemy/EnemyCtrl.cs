using UnityEngine;

public class EnemyCtrl : AbilityObjectCtrl
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }

    //[Header("Enemy")]
    /* [SerializeField] protected ShootableSpecialObjectSO shootableSpecialObjectSO;
    public ShootableSpecialObjectSO ShootableSpecialObjectSO => shootableSpecialObjectSO;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpecialSO();
    }

    protected virtual void LoadSpecialSO()
    {
        if (this.shootableSpecialObjectSO != null) return;
        string resPath = "ShootableSpecialObject/ShootableSpecialObjectSO";
        this.shootableSpecialObjectSO = Resources.Load<ShootableSpecialObjectSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadSpecialSO " + resPath, gameObject);
    } */
}
