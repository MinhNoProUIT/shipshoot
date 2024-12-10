using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelByKillEnemy : BaseMonoBehaviour
{
    [Header("Level")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    public SpawnerCtrl SpawnerCtrl => spawnerCtrl;
    private static LevelByKillEnemy instance;
    public static LevelByKillEnemy Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (LevelByKillEnemy.instance != null) Debug.LogError("Only 1 LevelByKillEnemy allow to exist");
        LevelByKillEnemy.instance = this;
    }
    

    [SerializeField] protected LevelSO[] levelSO;
    [SerializeField] protected int levelCurrent = 1;
    public int LevelCurrent => levelCurrent;
    [SerializeField] protected int killCount = 0;
    public int KillCount => killCount;
    [SerializeField] protected int enemiesKilled = 0;

    protected override void Start()
    {
        base.Start();
        this.SetBaseLevel();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelSO();
        this.LoadSpawnerCtrl();
    }

    protected virtual void LoadSpawnerCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadSpawnerCtrl", gameObject);
    }

    protected virtual void LoadLevelSO()
    {
        if(this.levelSO.Length > 0) return;
        string sceneName = SceneManager.GetActiveScene().name;
        string resPath = "LevelSO/" + sceneName;
        this.levelSO = Resources.LoadAll<LevelSO>(resPath);
        Debug.Log(transform.name + ": LoadLevelSO " + resPath, gameObject);
    }

    protected virtual void SetBaseLevel()
    {
        this.levelCurrent = this.levelSO[0].level;
        this.killCount = this.levelSO[0].killPerLevel;
        this.spawnerCtrl.SpawnerRandom.SetRandomLimit(this.killCount);
    }

    public void EnemyKilled()
    {
        this.enemiesKilled++;
        if(this.enemiesKilled >= this.levelSO[this.levelCurrent - 1].killPerLevel)
        {
            this.LevelUp();
            this.enemiesKilled = 0;
        }
    }

    protected virtual void LevelUp()
    {
        this.levelCurrent++;
        if(this.levelCurrent <= this.levelSO.Length){
            this.killCount = this.levelSO[this.levelCurrent - 1].killPerLevel;
            this.spawnerCtrl.SpawnerRandom.SetRandomLimit(this.killCount);
        }
        Debug.Log("Level Up: " + this.levelCurrent);

        if(this.levelCurrent > this.levelSO.Length)
        {
            this.levelCurrent = this.levelSO.Length;
            Debug.Log("Max Level");
        }
    }

    /* [SerializeField] protected int killCount = 0;
    [SerializeField] protected int killPerLevel = 10;
    public int KillCount => killCount;

    protected virtual void FixedUpdate()
    {
        this.Leveling();
    }

    protected virtual void Leveling()
    {
        this.killCount = EnemySpawner.Instance.SpawnedCount;
        int newLevel = this.GetLevelByKill();
        this.LevelSet(newLevel);
    }

    protected virtual int GetLevelByKill()
    {
        return Mathf.FloorToInt(this.killCount / this.killPerLevel) + 1;
    }

    public virtual void SetKillPerLevel(int killPerLevel)
    {
        this.killPerLevel = killPerLevel;
    } */




}