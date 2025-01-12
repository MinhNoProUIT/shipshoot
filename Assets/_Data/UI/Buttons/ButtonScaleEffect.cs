using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScaleEffect : MonoBehaviour
{
    // Start is called before the first frame update
   private void Start()
    {
        StartScaleEffect();
    }

    private void StartScaleEffect()
    {
        // Lấy Transform của Button
        Transform buttonTransform = transform;

        // Chuỗi Tween
        Sequence sequence = DOTween.Sequence();

        // Scale lên 1.2 trong 1s
        //sequence.Append(buttonTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetEase(Ease.InOutQuad));

        // Trở về scale 1 trong 1s
        //sequence.Append(buttonTransform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutQuad));

        // Scale xuống 0.8 trong 1s
        sequence.Append(buttonTransform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 2f).SetEase(Ease.InOutQuad));

        // Trở về scale 1 trong 1s
        sequence.Append(buttonTransform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 2f).SetEase(Ease.InOutQuad));

        // Lặp lại chuỗi hiệu ứng
        sequence.SetLoops(-1);
    }
}
