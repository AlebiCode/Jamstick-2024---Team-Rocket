using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Entity
{
    private float controlSpeed = 0;
    private float targetControlSpeed = 1;
    private float randomControlAcc = 1;

    private float terrainSpeed = 1;

    private RobotConstructor robotConstructor = new RobotConstructor();

    public float terrainAdvancement;

    private Weapon Weapon => robotConstructor.SelectedAttackPart.Weapon;
    private bool HasTarget => Weapon.HasTarget;
    public float MyX => transform.position.x;



    void Update()
    {
        if (controlSpeed == 0 && targetControlSpeed == 0)
            return;
        controlSpeed = Mathf.MoveTowards(controlSpeed, targetControlSpeed, randomControlAcc * Time.deltaTime);
        float movement = controlSpeed * terrainSpeed * Time.deltaTime;
        transform.position += Vector3.right * movement;
        terrainAdvancement += movement;
        if (terrainAdvancement >= TerrainGenerator.TERRAIN_WIDTH)
        {
            OnNewTerrainReached();
            terrainAdvancement -= TerrainGenerator.TERRAIN_WIDTH;
        }
        GameManager.instance.Advancement(MyX);
    }

    public void Initialize()
    {
        robotConstructor.Spawn(this);
    }
    public void Activate()
    {
        terrainAdvancement = MyX % TerrainGenerator.TERRAIN_WIDTH;
        OnFirstPlacement();
        enabled = true;
        //RollAccelleration();
    }
    private void OnFirstPlacement()
    {
        var myId = GameManager.TerrainGenerator.GetTerrainIDAtX(MyX);
        Terrain t = GameManager.TerrainGenerator.GetTerrainAtId(myId);
        t.AddRobot(this);
        UpdateSpeed(t.TerrainType);
        for (int i = myId; i < myId + GameManager.ENEMYBASE_RANGE; i++)
        {
            var nextTerrain = GameManager.TerrainGenerator.GetTerrainAtId(i);
            if (!nextTerrain.HasEnemyBase)
                continue;
            if (i - myId < GameManager.ENEMYBASE_RANGE)
            {
                nextTerrain.EnemyBase.OnBotEnteredMyRange(this);
            }
            if (i - myId < GameManager.ROBOT_RANGE)
                TargetCheck(nextTerrain.EnemyBase);
        }
    }
    private void OnNewTerrainReached()
    {
        int newId = GameManager.TerrainGenerator.GetTerrainIDAtX(MyX);
        GameManager.TerrainGenerator.GetTerrainAtId(newId - 1).RemoveRobot(this);
        var terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId);
        terrain.AddRobot(this);
        UpdateSpeed(terrain.TerrainType);
        terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId + GameManager.ENEMYBASE_RANGE - 1);
        if (terrain.HasEnemyBase)
            terrain.EnemyBase.OnBotEnteredMyRange(this);
        terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId + GameManager.ROBOT_RANGE - 1);
        if (terrain.HasEnemyBase)
            TargetCheck(terrain.EnemyBase);
    }

    public void TargetCheck(EnemyBase enemyBase)
    {
        if (!enemyBase.HasEnemies)
            return;
        BattleStance();
        AquireTarget(enemyBase);
    }
    public void OnTagetLost(EnemyBase enemyBase)
    {
        if (enemyBase.HasEnemies)
        {
            AquireTarget(enemyBase);
        }
        else
        {
            MovementStance();
            Weapon.Disengage();
        }
    }
    private void AquireTarget(EnemyBase enemyBase)
    {
        if (!IsAlive)
            return;
        Enemy target = enemyBase.GetClosestEnemyToZ(transform.position.z);
        Weapon.EngageTarget(target.transform);
        target.OnDeath.AddListener(() => OnTagetLost(enemyBase));
    }
    private void BattleStance()
    {
        targetControlSpeed = 0;
    }
    private void MovementStance()
    {
        targetControlSpeed = 1;
        RollAccelleration();
    }

    protected override void RecieveForce(Vector3 dir)
    {
        BoomForceOnDebris(robotConstructor.SelectedAttackPart.Rigidbody);
        BoomForceOnDebris(robotConstructor.SelectedDefencePart.Rigidbody);
        BoomForceOnDebris(robotConstructor.SelectedMovementPart.Rigidbody);
    }
    private void BoomForceOnDebris(Rigidbody targetRb)
    {
        targetRb.AddExplosionForce(1, transform.position + new Vector3(Random.Range(-0.33f, 0.33f), Random.Range(-0.33f, 0.33f)), 1);
    }

    protected override float ApplyDamageModifiers(float damage, AttacksKeys atkKey)
    {
        return damage * GameManager.GetDamageMultiplier(atkKey, robotConstructor.SelectedDefencePart.selectedDefence);
    }
    protected override void Death()
    {
        GameManager.TerrainGenerator.GetTerrainAtX(MyX).RemoveRobot(this);
        base.Death();
        Weapon.Disengage();
        enabled = false;
        robotConstructor.EnablePhysics();
        StartCoroutine(Despawner());
    }

    private IEnumerator Despawner()
    {
        yield return new WaitForSeconds(5);
        robotConstructor.Despawn();
        Destroy(this);
    }

    private void RollAccelleration()
    {
        randomControlAcc = Random.Range(0.5f, 1.5f);
    }
    private void UpdateSpeed(TerrainType terrainType)
    {
        terrainSpeed = GetSpeed(terrainType);
    }
    private float GetSpeed(TerrainType terrainType)
    {
        switch (robotConstructor.SelectedMovementPart.selectedMovement)
        {
            default:
            case MovementsKeys.PAWS:
                switch (terrainType)
                {
                    default:
                    case TerrainType.grass: return GameManager.HIGH_BOT_SPEED; 
                    case TerrainType.road: return GameManager.MID_BOT_SPEED; 
                    case TerrainType.mud: return GameManager.LOW_BOT_SPEED; 
                }
            case MovementsKeys.TRACKS:
                switch (terrainType)
                {
                    default:
                    case TerrainType.mud: return GameManager.HIGH_BOT_SPEED;
                    case TerrainType.grass: return GameManager.MID_BOT_SPEED;
                    case TerrainType.road: return GameManager.LOW_BOT_SPEED;
                }
            case MovementsKeys.WHEELS:
                switch (terrainType)
                {
                    default:
                    case TerrainType.road: return GameManager.HIGH_BOT_SPEED;
                    case TerrainType.mud: return GameManager.MID_BOT_SPEED;
                    case TerrainType.grass: return GameManager.LOW_BOT_SPEED;
                }
        }
    }

}