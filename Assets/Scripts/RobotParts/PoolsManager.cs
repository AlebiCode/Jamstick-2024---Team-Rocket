using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolsManager
{
    [SerializeField] private int robotPartsPoolLimit;
    [SerializeField] private int bulletsPoolLimit;
    [SerializeField] private Transform poolParent;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private RobotPart[] attackPrefabs;
    [SerializeField] private RobotPart[] defencePrefabs;
    [SerializeField] private RobotPart[] movementPrefabs;

    private List<GameObject> projectileBulletPool = new();
    private List<GameObject> explosiveBulletPool = new();
    private List<GameObject> energyBulletPool = new();

    private List<RobotPart> projectilesAttackPool = new();
    private List<RobotPart> explosivesAttackPool = new();
    private List<RobotPart> energyAttackPool = new();
    private List<RobotPart> armorDefencePool = new();
    private List<RobotPart> foamDefencePool = new();
    private List<RobotPart> shieldDefencePool = new();
    private List<RobotPart> pawsMovementPool = new();
    private List<RobotPart> tracksMovementPool = new();
    private List<RobotPart> wheelsMovementPool = new();

    public void GeneratePools()
    {
        for (int i = 0; i < robotPartsPoolLimit; i++)
        {
            InstantiateRobotPartPool(projectilesAttackPool, attackPrefabs[0]);
            InstantiateRobotPartPool(explosivesAttackPool, attackPrefabs[1]);
            InstantiateRobotPartPool(energyAttackPool, attackPrefabs[2]);

            InstantiateRobotPartPool(armorDefencePool, defencePrefabs[0]);
            InstantiateRobotPartPool(foamDefencePool, defencePrefabs[1]);
            InstantiateRobotPartPool(shieldDefencePool, defencePrefabs[2]);

            InstantiateRobotPartPool(pawsMovementPool, movementPrefabs[0]);
            InstantiateRobotPartPool(tracksMovementPool, movementPrefabs[1]);
            InstantiateRobotPartPool(wheelsMovementPool, movementPrefabs[2]);

        }

        for (int i = 0; i < bulletsPoolLimit; i++)
        {
            InstantiateGOPool(projectileBulletPool, bulletPrefabs[0]);
            InstantiateGOPool(explosiveBulletPool, bulletPrefabs[1]);
            InstantiateGOPool(energyBulletPool, bulletPrefabs[2]);
        }
    }

    private void InstantiateRobotPartPool(List<RobotPart> pool, RobotPart prefab)
    {
        RobotPart robotPart = Object.Instantiate(prefab, poolParent);
        pool.Add(robotPart);
        robotPart.gameObject.SetActive(false);
    }
    private void InstantiateGOPool(List<GameObject> pool, GameObject prefab)
    {
        GameObject robotPart = Object.Instantiate(prefab, poolParent);
        pool.Add(robotPart);
        robotPart.gameObject.SetActive(false);
    }

    public RobotPart GetAttackGameObject(AttacksKeys attackKey)
    {
        switch (attackKey)
        {
            default:
                return RetrieveFromPool(projectilesAttackPool, attackPrefabs[0]);

            case AttacksKeys.EXPLOSIVES:
                return RetrieveFromPool(explosivesAttackPool, attackPrefabs[1]);

            case AttacksKeys.ENERGY:
                return RetrieveFromPool(energyAttackPool, attackPrefabs[2]);
        }
    }

    public RobotPart GetDefenceGameObject(DefencesKeys defenceKey)
    {
        switch (defenceKey)
        {
            default:
                return RetrieveFromPool(armorDefencePool, defencePrefabs[0]);

            case DefencesKeys.FOAM:
                return RetrieveFromPool(foamDefencePool, defencePrefabs[1]);

            case DefencesKeys.SHIELDS:
                return RetrieveFromPool(shieldDefencePool, defencePrefabs[2]);
        }
    }

    public RobotPart GetMovementGameObject(MovementsKeys movementKey)
    {
        switch (movementKey)
        {
            default:
                return RetrieveFromPool(pawsMovementPool, movementPrefabs[0]);

            case MovementsKeys.TRACKS:
                return RetrieveFromPool(tracksMovementPool, movementPrefabs[1]);

            case MovementsKeys.WHEELS:
                return RetrieveFromPool(wheelsMovementPool, movementPrefabs[2]);
        }
    }

    public GameObject GetBulletGameObject(AttacksKeys attackKey)
    {
        switch (attackKey)
        {
            default:
                return RetrieveFromPool(projectileBulletPool, bulletPrefabs[0]);

            case AttacksKeys.EXPLOSIVES:
                return RetrieveFromPool(explosiveBulletPool, bulletPrefabs[1]);

            case AttacksKeys.ENERGY:
                return RetrieveFromPool(explosiveBulletPool, bulletPrefabs[2]);
        }
    }

    private RobotPart RetrieveFromPool(List<RobotPart> pool, RobotPart prefab)
    {
        foreach (RobotPart go in pool)
        {
            if (!go.gameObject.activeInHierarchy)
            {
                return go;
            }
        }
        RobotPart newInstance = Object.Instantiate(prefab, poolParent);
        pool.Add(newInstance);
        return newInstance;
    }
    private GameObject RetrieveFromPool(List<GameObject> pool, GameObject prefab)
    {
        foreach (GameObject go in pool)
        {
            if (!go.gameObject.activeInHierarchy)
            {
                return go;
            }
        }
        GameObject newInstance = Object.Instantiate(prefab, poolParent);
        pool.Add(newInstance);
        return newInstance;
    }

}
