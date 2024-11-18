using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSliderHP : BaseSlider
{
    [Header("HP")]
    [SerializeField] protected double maxHP = 100;
    [SerializeField] protected double currentHP = 50;

    protected override void FixedUpdate()
    {
        this.HPShowing();
    }

    protected virtual void HPShowing()
    {
        double hpPercent = this.currentHP / this.maxHP;
        this.slider.value = (float)hpPercent;
    }

    protected override void ValueSlider(float newValue)
    {
        //Debug.Log("newValue: " + newValue);
    }

    public virtual void SetMaxHp(double maxHP)
    {
        this.maxHP = maxHP;
    }

    public virtual void SetCurrentHp(double currentHP)
    {
        this.currentHP = currentHP;
    }

}
