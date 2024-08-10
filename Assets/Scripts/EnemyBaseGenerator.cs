using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyBaseGenerator
{
    [SerializeField] private EnemyBase enemyBasePrefab;
    [SerializeField] private Enemy enemyDudePrefab;
    [SerializeField] private Weapon enemyProjectileWeaponPrefab;
    [SerializeField] private Weapon enemyExplosiveWeaponPrefab;
    [SerializeField] private Weapon enemyEnergyWeaponPrefab;
    [SerializeField] private Transform enemyDudeParent;
    [SerializeField] private int minEnemyBaseDistance = 8;
    [SerializeField] private int minEnemyBaseBetweenDistance = 3;
    [Range(0f,1f)][SerializeField] private float enemyChance = 1;
    [SerializeField] private int minEnemies = 3;
    [SerializeField] private int maxEnemies = 6;

    private int maxAtLastGeneratedBase;

    public Enemy EnemyDudePrefab => enemyDudePrefab;

    public Enemy EnemyPerfab => enemyDudePrefab;
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
        enemyBase.Initialize(t, Random.Range(minEnemies, maxEnemies + 1), GetWeaponPrefab((AttacksKeys)Random.Range(0,3)), (DefencesKeys)Random.Range(0, 3));
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


}
