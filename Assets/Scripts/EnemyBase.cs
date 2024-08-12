using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    private const float Z_OFFSET = 3;
    private const float X_SPAWN_RANDOMNESS = 0.1f;

    //private List<Terrain> myTerrainsRange;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    private Terrain myTerrain;


    public bool HasEnemies => enemies.Count > 0;
    public Terrain MyTerrain => myTerrain;


    private Coroutine waitForTargeting;

    public void Initialize(Terrain terrain, Enemy enemyPrefab, Weapon weaponPrefab, int nEnemies)
    {
        myTerrain = terrain;
        SpawnEnemies(enemyPrefab, weaponPrefab, nEnemies);
    }

    private void SpawnEnemies(Enemy enemyPrefab, Weapon weaponPrefab, int quantity)
    {
        float minZPos = -(quantity - 1) * Z_OFFSET / 2;
        for (int i = 0; i < quantity; i++)
        {
            var enemy = Instantiate(enemyPrefab, GameManager.TerrainGenerator.EnemyBaseGenerator.EnemyDudeParent);
            enemy.transform.position = transform.position + new Vector3(Random.Range(-X_SPAWN_RANDOMNESS, X_SPAWN_RANDOMNESS), 0, minZPos + Z_OFFSET * i);
            enemy.Initialize(Instantiate(weaponPrefab));
            enemy.OnDeath.AddListener(() => OnEnemyDeath(enemy));
            enemies.Add(enemy);
        }
    }
    private void OnEnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            myTerrain.RemoveEnemyBase();
            GameManager.TerrainGenerator.EnemyBaseGenerator.OnBaseDefeated(this);
        }
    }

    public void OnBotEnteredMyRange(Robot robot)
    {
        if (waitForTargeting != null)
            return;
        waitForTargeting = StartCoroutine(WaitForTargeting());
    }

    private IEnumerator WaitForTargeting()
    {
        yield return null;
        foreach (var enemy in enemies)
        {
            if (enemy.HasTarget)
                continue;
            enemy.FindTarget(myTerrain);
        }
        waitForTargeting = null;
    }

    public Enemy GetClosestEnemyToZ(float z)
    {
        return GameManager.GetClosestToZ(enemies, z);
    }

}
