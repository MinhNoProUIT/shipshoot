using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPasswordFailed : BaseMonoBehaviour
{
    [SerializeField] protected Transform background;
    [SerializeField] protected Button ok;
    [SerializeField] protected Transform resetPasswordManager;

    private Coroutine autoProceedCoroutine;

    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Login Failed is called");
        AppearanceEffect();
        
        // Bắt đầu Coroutine để chuyển cảnh sau 3 giây nếu không nhấn OK
        autoProceedCoroutine = StartCoroutine(AutoProceedAfterDelay(3f));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CloseEffect();

        // Hủy Coroutine nếu OnDisable được gọi trước khi hết thời gian
        if (autoProceedCoroutine != null)
        {
            StopCoroutine(autoProceedCoroutine);
        }
    }

    protected virtual void AppearanceEffect()
    {
        background.transform.localScale = Vector3.zero; // Đặt scale ban đầu là 0,0,0
        background.transform.DOScale(new Vector3(6, 9, 1), 0.5f).SetEase(Ease.OutBack);
    }

    protected virtual void CloseEffect()
    {
        background.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack); // Scale xuống 0,0,0 trong 0.5s
    }

    protected override void Start()
    {
        base.Start();
        ok.onClick.AddListener(OnOkClicked);
    }

    protected virtual void OnOkClicked()
    {
        // Hủy Coroutine nếu người dùng nhấn nút OK
        if (autoProceedCoroutine != null)
        {
            StopCoroutine(autoProceedCoroutine);
        }

        Debug.Log("Chuyển sang màn hình đăng nhập");
        resetPasswordManager.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private IEnumerator AutoProceedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Tự động gọi hàm OnOkClicked sau khi hết thời gian chờ
        OnOkClicked();
    }
}
