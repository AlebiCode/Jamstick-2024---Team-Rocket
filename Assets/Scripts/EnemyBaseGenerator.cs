using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBaseGenerator
{
    [SerializeField] private Transform enemyDudeParent;
    [SerializeField] private Enemy enemyDudePrefab;
    [SerializeField] private int minEnemies = 3;
    [SerializeField] private int maxEnemies = 6;

    public Enemy EnemyPerfab => enemyDudePrefab;
    public Transform EnemyDudeParent => enemyDudeParent;

    public EnemyBase GenerateBase(Transform parent, Terrain t)
    {
        EnemyBase enemyBase = MonoBehaviour.Instantiate(GameManager.TerrainGenerator.EnemyBasePrefab, parent, true);
        enemyBase.transform.localPosition = Vector3.zero;
        enemyBase.Initialize(t, enemyDudePrefab, Random.Range(minEnemies, maxEnemies + 1));
        return enemyBase;
    }

}
