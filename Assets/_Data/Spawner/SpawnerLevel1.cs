using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab của đối tượng cần spawn
    public int numRows = 3; // Số hàng
    public int numCols = 4; // Số cột
    public float spacing = 1f; // Khoảng cách giữa các đối tượng
    public Vector2 spawnAreaCenter = Vector2.zero; // Tâm của vùng spawn
    public Vector2 spawnAreaSize = new Vector2(5f, 3f); // Kích thước vùng spawn
    public Vector2 startPosition = Vector2.zero;

    protected virtual void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        //OnDrawGizmosSelected();
        // Tính toán vị trí bắt đầu spawn
        //Vector2 startPosition = spawnAreaCenter - spawnAreaSize / 2f + new Vector2(spacing/2f, spacing/2f);
        
        startPosition.x = spawnAreaCenter.x - spawnAreaSize.x / 2f;
        startPosition.y = spawnAreaCenter.y + spawnAreaSize.y / 2f;
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                // Tính toán vị trí hiện tại
                Vector2 spawnPosition = startPosition + new Vector2(col * spacing, -row * spacing);

                // Kiểm tra xem vị trí có nằm trong vùng spawn không (nếu cần)
                //if (spawnPosition.x >= spawnAreaCenter.x - spawnAreaSize.x/2 && spawnPosition.x <= spawnAreaCenter.x + spawnAreaSize.x/2 &&
                //spawnPosition.y >= spawnAreaCenter.y - spawnAreaSize.y/2 && spawnPosition.y <= spawnAreaCenter.y + spawnAreaSize.y/2 )
                //{
                    // Khởi tạo đối tượng
                    EnemySpawner.Instance.Spawn("Enemy_1", spawnPosition, Quaternion.identity);
                //}
            }
        }
    }

    //Vẽ vùng spawn trong Scene View để dễ quan sát
    protected virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}