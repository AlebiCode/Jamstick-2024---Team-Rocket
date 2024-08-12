using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public const int ROBOT_RANGE = 2;
    public const int ENEMYBASE_RANGE = 4;

    public const int ROBOT_COST = 50;
    public const int SINGLE_ENEMY_LOOT = 50;

    public const float FIRE_RATE = 1f;
    public const float BASE_DAMAGE = 1f;
    public const float HIGH_DAMAGE_MULT = 2f;
    public const float MID_DAMAGE_MULT = 1f;
    public const float LOW_DAMAGE_MULT = 0.5f;

    private const float BASE_BOT_SPEED = 1f;
    public const float HIGH_BOT_SPEED = BASE_BOT_SPEED * 2f;
    public const float MID_BOT_SPEED = BASE_BOT_SPEED * 1f;
    public const float LOW_BOT_SPEED = BASE_BOT_SPEED * 0.5f;

    public const float BOT_DEATH_FORCE = 10f;
    public const float TIME_TO_GAMEOVER = 5f;

    public static GameManager instance;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private Factory factory;
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private PoolsManager poolsManager;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int money = 1000;
    [SerializeField] private UnityEvent<int> onMoneyChange;

    public List<Robot> aliveRobots = new List<Robot>();
    private static Coroutine gameOverCo;

    public static CameraController CameraController => instance.cameraController;
    public static TerrainGenerator TerrainGenerator => instance.terrainGenerator;
    public static Factory Factory => instance.factory;
    public static PoolsManager PoolsManager => instance.poolsManager;

    private bool GameOverCondition => aliveRobots.Count <= 0 && Money < ROBOT_COST;

    private int Money
    {
        get { return money; }
        set {
            money = value;
            GameOverCheck();
            onMoneyChange.Invoke(money);
            moneyText.text = value.ToString();
        }
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        terrainGenerator.Initialize();
        poolsManager.GeneratePools();
        moneyText.text = Money.ToString();
        SetProductionState();
    }

    public void OnRobotSpawn(Robot robot)
    {
        aliveRobots.Add(robot);
        GameOverCheck();
    }
    public void OnRobotDeath(Robot robot)
    {
        aliveRobots.Remove(robot);
        GameOverCheck();
    }

    public void Advancement(float x)
    {
        cameraController.SetTargetX(x);
    }
    public void AddMoney(int quantity)
    {
        Money += quantity;
    }
    public int TryBuyRobots(int quantity)
    {
        int purchases = Mathf.Min(Money / ROBOT_COST, quantity);
        if (purchases <= 0)
            return 0;
        Money -= purchases * ROBOT_COST;
        return purchases;
    }

    public void DestroyAllRobots()
    {
        foreach (Robot robot in aliveRobots)
        {
            Destroy(robot);
        }
    }

    public static T GetClosestToZ<T>(List<T> list, float z) where T : MonoBehaviour
    {
        T closest = null;
        float dist = float.PositiveInfinity;
        for (int i = 0; i < list.Count; i++)
        {
            float newDist = Mathf.Abs(list[i].transform.position.z - z);
            if (newDist >= dist)
                continue;
            closest = list[i];
            dist = newDist;
        }
        return closest;
    }

    public static float GetDamageMultiplier(AttacksKeys attacksKey, DefencesKeys defencesKey)
    {
        switch (attacksKey)
        {
            default:
            case AttacksKeys.TRUE:
                return 1;
            case AttacksKeys.PROJECTILES:
                switch (defencesKey)
                {
                    default:
                    case DefencesKeys.FOAM: return HIGH_DAMAGE_MULT;
                    case DefencesKeys.SHIELDS: return MID_DAMAGE_MULT;
                    case DefencesKeys.ARMOR: return LOW_DAMAGE_MULT;
                }
            case AttacksKeys.EXPLOSIVES:
                switch (defencesKey)
                {
                    default:
                    case DefencesKeys.SHIELDS: return HIGH_DAMAGE_MULT;
                    case DefencesKeys.ARMOR: return MID_DAMAGE_MULT;
                    case DefencesKeys.FOAM: return LOW_DAMAGE_MULT;
                }
            case AttacksKeys.ENERGY:
                switch (defencesKey)
                {
                    default:
                    case DefencesKeys.ARMOR: return HIGH_DAMAGE_MULT;
                    case DefencesKeys.FOAM: return MID_DAMAGE_MULT;
                    case DefencesKeys.SHIELDS: return LOW_DAMAGE_MULT;
                }
        }
    }

    public void SetProductionState()
    {
        factory.ToggleActivation();
    }
    private void GameOverCheck()
    {
        if (GameOverCondition)
        {
            if (gameOverCo != null)
                return;
            gameOverCo = StartCoroutine(GameOverCo());
        }
        else
        {
            if (gameOverCo == null)
                return;
            StopCoroutine(gameOverCo);
            gameOverCo = null;
        }
    }

    private IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(TIME_TO_GAMEOVER);
        Debug.Log("GameOver!");
    }

}
