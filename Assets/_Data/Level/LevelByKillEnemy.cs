using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelByKillEnemy : BaseMonoBehaviour
{
    [Header("Level")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    public SpawnerCtrl SpawnerCtrl => spawnerCtrl;
    private static LevelByKillEnemy instance;
    public static LevelByKillEnemy Instance => instance;

    [SerializeField] protected LevelSO[] levelSO;
    [SerializeField] protected int levelCurrent = 1;
    public int LevelCurrent => levelCurrent;
    [SerializeField] protected int enemiesKilled = 0;
    [SerializeField] protected BossCtrl bossCtrl;

    [Header("UI")]
    public GameObject levelUpUI;  // UI hiển thị khi lên level
    public GameObject bossUI;
    [SerializeField] protected int countShowUILevel = 1;
    
    [SerializeField]public GameObject Countdowntime;    // UI hiển thị khi đến boss level

    protected override void Awake()
    {
        base.Awake();
        if (LevelByKillEnemy.instance != null) Debug.LogError("Only 1 LevelByKillEnemy allowed to exist");
        LevelByKillEnemy.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.SetBaseLevel();
        this.ShowLevelUpUI();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelSO();
        this.LoadSpawnerCtrl();
        this.LoadBossCtrl();
    }

    protected virtual void LoadBossCtrl(){
        if (this.bossCtrl != null) return;
        this.bossCtrl = GameObject.Find("Boss").GetComponent<BossCtrl>();
        Debug.LogWarning(transform.name + ": LoadBossCtrl", gameObject);
    }

    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadSpawnerCtrl", gameObject);
    }

    protected virtual void LoadLevelSO()
    {
        if (this.levelSO.Length > 0) return;
        string sceneName = SceneManager.GetActiveScene().name;
        string resPath = "LevelSO/" + sceneName;
        this.levelSO = Resources.LoadAll<LevelSO>(resPath);
        Debug.Log(transform.name + ": LoadLevelSO " + resPath, gameObject);
    }

    protected virtual void SetBaseLevel()
    {
        this.levelCurrent = this.levelSO[0].level;
        this.spawnerCtrl.SpawnerRandom.SetRandomLimit(this.levelSO[0].killPerLevel);
    }

    public void EnemyKilled()
    {
        this.enemiesKilled++;
        if(levelCurrent > this.levelSO.Length) {
            this.ShowBossUI();
            return;
        }
        if (this.enemiesKilled >= this.levelSO[this.levelCurrent - 1].killPerLevel)
        {
            this.LevelUp();
            this.enemiesKilled = 0;
        }
    }

    protected virtual void LevelUp()
    {
        this.levelCurrent++;
        if(this.levelCurrent > this.levelSO.Length) return;
        // Kiểm tra nếu đã vượt quá số level trong LevelSO
       /*  if (this.levelCurrent > this.levelSO.Length)
        {
            int countdownTime = FindObjectOfType<CountdownTimer>()?.countdownTime ?? 0;

            PlayerPrefs.SetInt("CountdownTime", countdownTime);

            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                foreach (var item in inventory.Items)
                {
                    string itemKey = item.itemProfile.itemCode.ToString();
                    PlayerPrefs.SetInt(itemKey, item.itemCount);
                    Debug.Log(itemKey + ":" + item.itemCount);
                }
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene("ScoreScene");
            Debug.Log("Reached Boss Level!");
            this.ShowBossUI();
            return;
        }
 */
        // Cập nhật số enemy cần giết cho level tiếp theo
        this.spawnerCtrl.SpawnerRandom.SetRandomLimit(this.levelSO[this.levelCurrent - 1].killPerLevel);

        // Hiển thị UI lên level
        if(countShowUILevel < this.levelSO.Length) this.ShowLevelUpUI();
        countShowUILevel ++;

        Debug.Log("Level Up: " + this.levelCurrent);
    }

    protected virtual void ShowLevelUpUI()
    {
        if (levelUpUI != null)
        {
            levelUpUI.SetActive(true);
            Invoke(nameof(HideLevelUpUI), 3f); // Hiển thị UI trong 2 giây
        }
    }

    protected virtual void HideLevelUpUI()
    {
        if (levelUpUI != null)
        {
            levelUpUI.SetActive(false);
        }
    }


    protected virtual void ShowBossUI()
    {
        if (bossUI != null)
        {
            bossUI.SetActive(true);
            Invoke(nameof(SpawnBoss), 3f); // Hiển thị UI trước khi spawn boss
        }
    }

    protected virtual void SpawnBoss()
    {
        bossUI.SetActive(false);

        if(bossCtrl == null) return;
        bossCtrl.gameObject.SetActive(true);
    }
}
