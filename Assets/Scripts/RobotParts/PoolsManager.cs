using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    [SerializeField] private int robotPartsPoolLimit;
    [SerializeField] private int bulletsPoolLimit;
    [SerializeField] private GameObject[] bulletPrefabs;
    [SerializeField] private GameObject[] attackPrefabs;
    [SerializeField] private GameObject[] defencePrefabs;
    [SerializeField] private GameObject[] movementPrefabs;

    [SerializeField] private List<GameObject> projectileBulletPool = new();
    [SerializeField] private List<GameObject> explosiveBulletPool = new();
    [SerializeField] private List<GameObject> energyBulletPool = new();
    [SerializeField] private List<GameObject> projectilesAttackPool = new();
    [SerializeField] private List<GameObject> explosivesAttackPool = new();
    [SerializeField] private List<GameObject> energyAttackPool = new();
    [SerializeField] private List<GameObject> armorDefencePool = new();
    [SerializeField] private List<GameObject> foamDefencePool = new();
    [SerializeField] private List<GameObject> shieldDefencePool = new();
    [SerializeField] private List<GameObject> pawsMovementPool = new();
    [SerializeField] private List<GameObject> tracksMovementPool = new();
    [SerializeField] private List<GameObject> wheelsMovementPool = new();

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
            GameObject projectile = Instantiate(attackPrefabs[0]);
            projectilesAttackPool.Add(projectile);
            projectile.SetActive(false);

            GameObject explosive = Instantiate(attackPrefabs[1]);
            explosivesAttackPool.Add(explosive);
            explosive.SetActive(false);

            GameObject energy = Instantiate(attackPrefabs[2]);
            energyAttackPool.Add(energy);
            energy.SetActive(false);

            GameObject armor = Instantiate(defencePrefabs[0]);
            armorDefencePool.Add(armor);
            armor.SetActive(false);

            GameObject foam = Instantiate(defencePrefabs[1]);
            foamDefencePool.Add(foam);
            foam.SetActive(false);

            GameObject shield = Instantiate(defencePrefabs[2]);
            shieldDefencePool.Add(shield);
            shield.SetActive(false);

            GameObject paws = Instantiate(movementPrefabs[0]);
            pawsMovementPool.Add(paws);
            paws.SetActive(false);

            GameObject tracks = Instantiate(movementPrefabs[1]);
            tracksMovementPool.Add(tracks);
            tracks.SetActive(false);

            GameObject wheels = Instantiate(movementPrefabs[2]);
            wheelsMovementPool.Add(wheels);
            wheels.SetActive(false);
        }

        for (int i = 0; i < bulletsPoolLimit; i++)
        {
            GameObject projectile = Instantiate(bulletPrefabs[0]);
            projectileBulletPool.Add(projectile);
            projectile.SetActive(false);

            GameObject explosive = Instantiate(bulletPrefabs[1]);
            explosiveBulletPool.Add(explosive);
            explosive.SetActive(false);

            GameObject energy = Instantiate(bulletPrefabs[2]);
            energyBulletPool.Add(energy);
            energy.SetActive(false);
        }
    }

    public GameObject GetAttackGameObject(AttacksKeys attackKey)
    {
        switch (attackKey)
        {
            default:
                foreach (GameObject projectile in projectilesAttackPool)
                {
                    if (!projectile.activeInHierarchy)
                    {
                        return projectile;
                    }
                }
                GameObject newProjectile = Instantiate(attackPrefabs[0]);
                projectilesAttackPool.Add(newProjectile);
                return newProjectile;

            case AttacksKeys.EXPLOSIVES:
                foreach (GameObject explosive in explosivesAttackPool)
                {
                    if (!explosive.activeInHierarchy)
                    {
                        return explosive;
                    }
                }
                GameObject newExplosive = Instantiate(attackPrefabs[1]);
                explosivesAttackPool.Add(newExplosive);
                return newExplosive;

            case AttacksKeys.ENERGY:
                foreach (GameObject energy in energyAttackPool)
                {
                    if (!energy.activeInHierarchy)
                    {
                        return energy;
                    }
                }
                GameObject newEnergy = Instantiate(attackPrefabs[2]);
                energyAttackPool.Add(newEnergy);
                return newEnergy;
        }
    }

    public GameObject GetDefenceGameObject(DefencesKeys defenceKey)
    {
        switch (defenceKey)
        {
            default:
                foreach (GameObject armor in armorDefencePool)
                {
                    if (!armor.activeInHierarchy)
                    {
                        return armor;
                    }
                }
                GameObject newArmor = Instantiate(defencePrefabs[0]);
                armorDefencePool.Add(newArmor);
                return newArmor;

            case DefencesKeys.FOAM:
                foreach (GameObject foam in foamDefencePool)
                {
                    if (!foam.activeInHierarchy)
                    {
                        return foam;
                    }
                }
                GameObject newFoam = Instantiate(defencePrefabs[1]);
                foamDefencePool.Add(newFoam);
                return newFoam;

            case DefencesKeys.SHIELDS:
                foreach (GameObject shield in shieldDefencePool)
                {
                    if (!shield.activeInHierarchy)
                    {
                        return shield;
                    }
                }
                GameObject newShield = Instantiate(defencePrefabs[2]);
                shieldDefencePool.Add(newShield);
                return newShield;
        }
    }

    public GameObject GetMovementGameObject(MovementsKeys movementKey)
    {
        switch (movementKey)
        {
            default:
                foreach (GameObject paws in pawsMovementPool)
                {
                    if (!paws.activeInHierarchy)
                    {
                        return paws;
                    }
                }
                GameObject newPaws = Instantiate(movementPrefabs[0]);
                pawsMovementPool.Add(newPaws);
                return newPaws;

            case MovementsKeys.TRACKS:
                foreach (GameObject tracks in tracksMovementPool)
                {
                    if (!tracks.activeInHierarchy)
                    {
                        return tracks;
                    }
                }
                GameObject newTracks = Instantiate(movementPrefabs[1]);
                tracksMovementPool.Add(newTracks);
                return newTracks;

            case MovementsKeys.WHEELS:
                foreach (GameObject wheels in wheelsMovementPool)
                {
                    if (!wheels.activeInHierarchy)
                    {
                        return wheels;
                    }
                }
                GameObject newWheels = Instantiate(movementPrefabs[2]);
                wheelsMovementPool.Add(newWheels);
                return newWheels;
        }
    }

    public GameObject GetBulletGameObject(AttacksKeys attackKey)
    {
        switch (attackKey)
        {
            default:
                foreach (GameObject projectile in projectileBulletPool)
                {
                    if (!projectile.activeInHierarchy)
                    {
                        return projectile;
                    }
                }
                GameObject newProjectile = Instantiate(bulletPrefabs[0]);
                projectileBulletPool.Add(newProjectile);
                return newProjectile;

            case AttacksKeys.EXPLOSIVES:
                foreach (GameObject explosive in explosiveBulletPool)
                {
                    if (!explosive.activeInHierarchy)
                    {
                        return explosive;
                    }
                }
                GameObject newExplosive = Instantiate(bulletPrefabs[1]);
                explosiveBulletPool.Add(newExplosive);
                return newExplosive;

            case AttacksKeys.ENERGY:
                foreach (GameObject energy in energyBulletPool)
                {
                    if (!energy.activeInHierarchy)
                    {
                        return energy;
                    }
                }
                GameObject newEnergy = Instantiate(bulletPrefabs[2]);
                energyBulletPool.Add(newEnergy);
                return newEnergy;
        }
    }
}
