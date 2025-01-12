using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemRotation : BaseMonoBehaviour
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.DORotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear) // Chuyển động tuyến tính
                 .SetLoops(-1, LoopType.Restart);
    }
}
