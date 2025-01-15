using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIManager : BaseMonoBehaviour
{
    [Header("UI Elements")]
    public GameObject uiPanel; // UI cần hiển thị

    [Header("Animation Settings")]
    public float animationDuration = 1f; // Thời gian phóng to và thu nhỏ
    public float displayDuration = 3f;   // Thời gian hiển thị nguyên trạng

    private static UIManager instance;
    public static UIManager Instance => instance;
    private Vector3 originalScale;

     protected override void Awake()
    {
        base.Awake();
        if (UIManager.instance != null) Debug.LogError("Only 1 LevelByKillEnemy allowed to exist");
        UIManager.instance = this;
    }

    protected override void Start()
    {
        // Lưu lại scale ban đầu của UI
        if (uiPanel != null)
        {
            originalScale = uiPanel.transform.localScale;
            uiPanel.transform.localScale = Vector3.zero; // Ẩn UI ban đầu
        }
    }

    public void ShowUI()
    {
        if (uiPanel == null) return;

        // Tạm dừng game
        Time.timeScale = 0f;

        // Hiển thị UI từ scale (0,0,0) đến (1,1,1)
        uiPanel.SetActive(true);
        uiPanel.transform.localScale = Vector3.zero;
        uiPanel.transform.DOScale(originalScale, animationDuration).SetUpdate(true).OnComplete(() =>
        {
            // Giữ nguyên UI trong một khoảng thời gian
            StartCoroutine(HoldUI());
        });
    }

    private IEnumerator HoldUI()
    {
        yield return new WaitForSecondsRealtime(displayDuration);

        // Thu nhỏ UI về (0,0,0)
        uiPanel.transform.DOScale(Vector3.zero, animationDuration).SetUpdate(true).OnComplete(() =>
        {
            uiPanel.SetActive(false);

            // Tiếp tục game
            Time.timeScale = 1f;
        });
    }
}
