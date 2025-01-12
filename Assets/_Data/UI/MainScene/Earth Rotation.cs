using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EarthRotation : BaseMonoBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) // Chuyển động tuyến tính
                 .SetLoops(-1, LoopType.Restart);
    }
}
