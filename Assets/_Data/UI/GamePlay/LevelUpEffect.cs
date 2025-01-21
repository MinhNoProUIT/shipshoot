using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelUpEffect : BaseMonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Transform background;


    protected override void OnEnable()
    {
        base.OnEnable();
        AppearanceEffect();
        // Bắt đầu Coroutine để chuyển cảnh sau 3 giây nếu không nhấn OK
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CloseEffect();
        // Hủy Coroutine nếu OnDisable được gọi trước khi hết thời gian
       
    }

    protected virtual void AppearanceEffect()
    {
        background.transform.localScale = Vector3.zero; // Đặt scale ban đầu là 0,0,0
        background.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBack);
    }

    protected virtual void CloseEffect()
    {
        background.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        Debug.LogWarning("On Disable is called");
    }
}
