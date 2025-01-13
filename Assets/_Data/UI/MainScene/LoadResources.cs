using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResources : BaseMonoBehaviour
{
    // Start is called before the first frame update
    protected override void Awake(){
        base.Awake();
        DatabaseManager.Instance.LoadResources(DatabaseManager.Instance.GetUserId(), success =>
        {
            if (success)
            {
                Debug.Log("Tải tài nguyên thành công! Chuyển sang Main Scene.");
                //SceneManager.LoadScene("MainScene");
            }
            else
            {
                Debug.LogError("Không thể tải tài nguyên từ cơ sở dữ liệu.");
            }
        });
    }
}
