using UnityEngine;

public class DamageSender : BaseMonoBehaviour
{
    [SerializeField] protected int damage = 1;

    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        Debug.Log("DamageReceiver: " + obj.name);
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        this.CreateImpactFX();
        damageReceiver.Deduct(this.damage);
    }

    protected virtual void CreateImpactFX()
    {
        string fxName = this.GetImpactFX();

        Vector3 hitPos = transform.position;
        Quaternion hitRot = transform.rotation;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
        fxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.impact1;
    }
}
