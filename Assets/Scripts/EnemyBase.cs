using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    private const float offsetZ = 3;

    //private List<Terrain> myTerrainsRange;
    [SerializeField] private List<Enemy> enemies = new List<Enemy>();
    private Terrain myTerrain;


    public bool HasEnemies => enemies.Count > 0;

    private Coroutine waitForTargeting;

    public void Initialize(Terrain terrain, Enemy enemyPrefab, int nEnemies)
    {
        myTerrain = terrain;
        SpawnEnemies(enemyPrefab, nEnemies);
    }

    private void SpawnEnemies(Enemy enemyPrefab, int quantity)
    {
        float minZPos = -(quantity - 1) * offsetZ / 2;
        for (int i = 0; i < quantity; i++)
        {
            var enemy = Instantiate(enemyPrefab, GameManager.TerrainGenerator.EnemyBaseGenerator.EnemyDudeParent);
            enemy.transform.position = transform.position + new Vector3(0, 0, minZPos + offsetZ * i);
            enemy.Initialize();
            enemy.OnDeath.AddListener(() => RemoveEnemy(enemy));
            enemies.Add(enemy);
        }
    }
    private void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
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
