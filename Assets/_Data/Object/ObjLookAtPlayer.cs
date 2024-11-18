using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLookAtPlayer : ObjLookAtTarget
{

    [Header("Look At Player")]
    [SerializeField] protected GameObject player;

    protected override void FixedUpdate()
    {
        this.GetMousePosition();
        base.FixedUpdate();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
        this.LoadValue();
    }

    protected virtual void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void GetMousePosition()
    {
        if (this.player == null) return;

        this.targetPosition = this.player.transform.position;
        this.targetPosition.z = 0;
    }
    protected virtual void LoadValue()
    {
        this.rotSpeed = 0.5f;
    }
    
}
