using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float minZPosition = -3;
    [SerializeField] private float maxZPosition = 3;

    //private List<Terrain> myTerrainsRange;
    private List<Enemy> enemies = new List<Enemy>();
    private Robot currentTarget;

    public void Update()
    {
        if (!currentTarget.IsAlive)
        {
            enabled = false;
            return;
        }
        Shoot();
    }

    public void Initialize(Terrain terrain, Enemy prefab, int nEnemies)
    {
        //myTerrainsRange = GameManager.TerrainGenerator.GetPreviousTerrains(terrain);
        SpawnEnemies(prefab, nEnemies);
    }

    private void SpawnEnemies(Enemy enemyPrefab, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            var enemy = Instantiate(enemyPrefab, GameManager.TerrainGenerator.EnemyBaseGenerator.EnemyDudeParent);
            enemy.transform.position = transform.position + new Vector3(0, 1, minZPosition + (i * ((maxZPosition - minZPosition) / (quantity - 1))));
            enemy.Initialize(null);
            enemies.Add(enemy);
        }
    }

    public void OnBotEnteredMyRange(Robot robot)
    {
        if (currentTarget != null)
            return;
        if (currentTarget == null)
            TargetClosestRobot();
    }

    private void TargetClosestRobot()   //also call when target robot dies!!
    {
        /*
        for (int i = myTerrainsRange.Count - 1; i >= 0; i++)
        {
            Robot r = myTerrainsRange[i].GetFurthestRobot();
            if (r != null)
            {
                currentTarget = r;
                enabled = true;
                return;
            }
        }
        enabled = false;
        */
    }

    private void Shoot()
    {

    }

    public Enemy GetClosestEnemy(float z)
    {
        Enemy closest = null;
        float dist = float.PositiveInfinity;
        for (int i = 0; i < enemies.Count; i++)
        {
            float newDist = Mathf.Abs(enemies[i].transform.position.z - z);
            if (newDist >= dist)
                continue;
            closest = enemies[i];
            dist = newDist;
        }
        return closest;
    }

}
