using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScreen;
    [SerializeField] protected Image loadingBar;
    [SerializeField] protected TextMeshProUGUI loadingText;

    public void LoadSceneAsync(string sceneName)
    {
        Debug.Log("Button clicked");

        if (SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.Log($"Scene '{sceneName}' đã được tải. Không cần tải lại.");
            return;
        }

        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true); // Hiển thị màn hình chờ
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            Task.Delay(1000);
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            if(loadingBar!=null)
            {
                loadingBar.fillAmount = progress;
            }
            if(loadingText!=null)
            {
                loadingText.text = $"{progress * 100}%";
            }
            Debug.Log("Loading Progress: " + progress);
            // Cập nhật tiến trình, nếu cần
            yield return null;
        }

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false); // Tắt màn hình chờ
        }
    }
}
