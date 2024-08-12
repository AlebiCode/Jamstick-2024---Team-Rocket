using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBaseGenerator
{
    [Header("References")]
    [SerializeField] private EnemyBase enemyBasePrefab;
    [SerializeField] private Enemy[] enemyDudePrefabs;
    [SerializeField] private Weapon enemyProjectileWeaponPrefab;
    [SerializeField] private Weapon enemyExplosiveWeaponPrefab;
    [SerializeField] private Weapon enemyEnergyWeaponPrefab;
    [SerializeField] private Transform enemyDudeParent;
    [SerializeField] private GameObject dangerLine;
    [Header("Values")]
    [SerializeField] private int minEnemyBaseDistance = 8;
    [SerializeField] private int minEnemyBaseBetweenDistance = 3;
    [Range(0f,1f)][SerializeField] private float enemyChance = 1;
    [SerializeField] private int minEnemies = 3;
    [SerializeField] private int maxEnemies = 6;


    private int maxAtLastGeneratedBase;

    public Enemy[] EnemyDudePrefabs => enemyDudePrefabs;

    public Transform EnemyDudeParent => enemyDudeParent;

    public EnemyBase TryGenerateBase(Transform parent, Terrain t)
    {
        if (GameManager.TerrainGenerator.MaxTerrainDistance < minEnemyBaseDistance ||
            GameManager.TerrainGenerator.MaxTerrainDistance - maxAtLastGeneratedBase <= 3 ||
            Random.Range(0, 1) > enemyChance)
            return null;
        maxAtLastGeneratedBase = GameManager.TerrainGenerator.MaxTerrainDistance;
        EnemyBase enemyBase = Object.Instantiate(enemyBasePrefab, parent, true);
        enemyBase.transform.localPosition = Vector3.zero;
        enemyBase.Initialize(t, GetRandomEnemy(), GetWeaponPrefab((AttacksKeys)Random.Range(0,3)), Random.Range(minEnemies, maxEnemies + 1));

        if(!dangerLine.activeInHierarchy)
            PositionDangerLine(t.transform);
        return enemyBase;
    }

    private Weapon GetWeaponPrefab(AttacksKeys attacksKey)
    {
        switch (attacksKey)
        {
            default:
            case AttacksKeys.PROJECTILES: return enemyProjectileWeaponPrefab;
            case AttacksKeys.EXPLOSIVES: return enemyExplosiveWeaponPrefab;
            case AttacksKeys.ENERGY: return enemyEnergyWeaponPrefab;
        }
    }
    private Enemy GetRandomEnemy()
    {
        return GameManager.TerrainGenerator.EnemyBaseGenerator.EnemyDudePrefabs[Random.Range(0,3)];
    }

    public void OnBaseDefeated(EnemyBase enemyBase)
    {
        var next = GameManager.TerrainGenerator.FindFirstEnemyBaseTerrain();
        if (next == null)
        {
            dangerLine.SetActive(false);
            return;
        }
        PositionDangerLine(next.transform);
    }

    private void PositionDangerLine(Transform target)
    {
        dangerLine.SetActive(true);
        dangerLine.transform.position = target.position + new Vector3(- (GameManager.ENEMYBASE_RANGE - 1) * 4, 0.2f);
    }

}
