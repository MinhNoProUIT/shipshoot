using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : BaseMonoBehaviour
{
    [Header("Base Slider")]
    [SerializeField] protected Slider slider;
    protected override void Start()
    {
        base.Start();
        this.AddOnClickListeners();
    }
    protected virtual void FixedUpdate() { }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSlider();
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = transform.GetComponent<Slider>();
        Debug.Log(transform.name + ": LoadSlider", gameObject);
    }

    protected virtual void AddOnClickListeners()
    {
        slider.onValueChanged.AddListener(this.ValueSlider);
    }

    protected abstract void ValueSlider(float newValue);

}
