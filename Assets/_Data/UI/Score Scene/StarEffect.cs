using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarEffect : BaseMonoBehaviour
{
    protected override void OnEnable()
    {
        base.OnEnable();
        CreateEffect();
    }

    protected virtual void CreateEffect(){
        transform.localScale = Vector3.zero;

        // Tạo hiệu ứng scale từ (0, 0, 0) đến (1.1, 1.1, 1.1) trong 1 giây
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1f).OnComplete(() =>
        {
            // Lặp lại hiệu ứng scale từ (1.1, 1.1, 1.1) về (0.9, 0.9, 0.9) trong 2 giây
            // và từ (0.9, 0.9, 0.9) lên (1.1, 1.1, 1.1) trong 2 giây
            transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 2f)
                .SetLoops(-1, LoopType.Yoyo); // -1 là lặp vô hạn, Yoyo để đảo chiều
        });
    }
}
