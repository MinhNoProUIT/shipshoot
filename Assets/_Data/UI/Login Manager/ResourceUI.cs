using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResourceUI : BaseMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldsText;
    [SerializeField] private TextMeshProUGUI diamondsText;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (DatabaseManager.Instance == null)
        {
            Debug.LogError("DatabaseManager.Instance is null! Ensure it is initialized before accessing.");
            return;
        }

        if (DatabaseManager.Instance.Golds == null || DatabaseManager.Instance.Diamonds == null)
        {
            Debug.LogError("Golds or Diamonds is null! Ensure they are properly initialized.");
            return;
        }
        DatabaseManager.Instance.Golds.OnValueChanged += UpdateGoldsUI;
        DatabaseManager.Instance.Diamonds.OnValueChanged += UpdateDiamondsUI;

        UpdateGoldsUI(DatabaseManager.Instance.Golds.Value);
        UpdateDiamondsUI(DatabaseManager.Instance.Diamonds.Value);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        DatabaseManager.Instance.Golds.OnValueChanged -= UpdateGoldsUI;
        DatabaseManager.Instance.Diamonds.OnValueChanged -= UpdateDiamondsUI;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void UpdateGoldsUI(int golds)
    {
        goldsText.text = golds.ToString();
    }

    private void UpdateDiamondsUI(int diamonds)
    {
        diamondsText.text = diamonds.ToString();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateGoldsUI(DatabaseManager.Instance.Golds.Value);
        UpdateDiamondsUI(DatabaseManager.Instance.Diamonds.Value);
    }
}