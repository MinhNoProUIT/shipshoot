using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpecialDespawn : DespawnByDistance
{
    // Start is called before the first frame update
    public override void DespawnObject()
    {
        ItemSpecialSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.disLimit = 20f;
    }
}
