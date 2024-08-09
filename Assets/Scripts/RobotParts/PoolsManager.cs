using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] private int robotPartsPoolLimit;
    [SerializeField] private int bulletsPoolLimit;
    [SerializeField] private Transform poolParent;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameObject[] attackPrefabs;
    [SerializeField] private GameObject[] defencePrefabs;
    [SerializeField] private GameObject[] movementPrefabs;

    private List<GameObject> projectileBulletPool = new();
    private List<GameObject> explosiveBulletPool = new();
    private List<GameObject> energyBulletPool = new();
    private List<GameObject> projectilesAttackPool = new();
    private List<GameObject> explosivesAttackPool = new();
    private List<GameObject> energyAttackPool = new();
    private List<GameObject> armorDefencePool = new();
    private List<GameObject> foamDefencePool = new();
    private List<GameObject> shieldDefencePool = new();
    private List<GameObject> pawsMovementPool = new();
    private List<GameObject> tracksMovementPool = new();
    private List<GameObject> wheelsMovementPool = new();

    public static PoolsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GeneratePools();
    }
    private void GeneratePools()
    {
        for (int i = 0; i < robotPartsPoolLimit; i++)
        {
            InstantiateToPool(projectilesAttackPool, attackPrefabs[0]);
            InstantiateToPool(explosivesAttackPool, attackPrefabs[1]);
            InstantiateToPool(energyAttackPool, attackPrefabs[2]);

            InstantiateToPool(armorDefencePool, defencePrefabs[0]);
            InstantiateToPool(foamDefencePool, defencePrefabs[1]);
            InstantiateToPool(shieldDefencePool, defencePrefabs[2]);

            InstantiateToPool(pawsMovementPool, movementPrefabs[0]);
            InstantiateToPool(tracksMovementPool, movementPrefabs[1]);
            InstantiateToPool(wheelsMovementPool, movementPrefabs[2]);

        }

        for (int i = 0; i < bulletsPoolLimit; i++)
        {
            InstantiateToPool(projectileBulletPool, bulletPrefabs[0]);
            InstantiateToPool(explosiveBulletPool, bulletPrefabs[1]);
            InstantiateToPool(energyBulletPool, bulletPrefabs[2]);
        }
    }

    private void InstantiateToPool(List<GameObject> pool, GameObject prefab)
    {
        GameObject projectile = Instantiate(prefab, poolParent);
        pool.Add(projectile);
        projectile.SetActive(false);
    }

    public GameObject GetAttackGameObject(AttacksKeys attackKey)
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

    public GameObject GetDefenceGameObject(DefencesKeys defenceKey)
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

    public GameObject GetMovementGameObject(MovementsKeys movementKey)
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

    private GameObject RetrieveFromPool(List<GameObject> pool, GameObject prefab)
    {
        foreach (GameObject go in pool)
        {
            if (!go.activeInHierarchy)
            {
                return go;
            }
        }
        GameObject newInstance = Instantiate(prefab, poolParent);
        projectilesAttackPool.Add(newInstance);
        return newInstance;
    }
}
