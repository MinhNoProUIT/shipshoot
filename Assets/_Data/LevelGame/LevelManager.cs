using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : BaseMonoBehaviour
{
    [SerializeField] public static LevelManager instance;
    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        while (!scene.isDone)
        {
            // Khi tiến độ đạt 90%, cho phép kích hoạt scene
            if (scene.progress >= 0.9f)
            {
                scene.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
