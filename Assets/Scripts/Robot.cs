using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] private float hp = 4;
    [SerializeField] private float speed = 4;

    private Queue<EnemyBase> targets = new Queue<EnemyBase>();

    public float terrainAdvancement;

    public float MyX => transform.position.x;
    public bool IsAlive => hp > 0;

    void Start()
    {
        Activate();
    }

    void Update()
    {
        float movement = speed * Time.deltaTime;
        transform.position += Vector3.right * movement;
        terrainAdvancement += movement;
        if (terrainAdvancement >= TerrainGenerator.TERRAIN_WIDTH)
        {
            OnNewTerrainReached();
            terrainAdvancement -= TerrainGenerator.TERRAIN_WIDTH;
        }
        GameManager.instance.Advancement(MyX);
    }
    
    private void Activate()
    {
        terrainAdvancement = MyX % TerrainGenerator.TERRAIN_WIDTH;
        OnFirstPlacement();
    }
    private void OnFirstPlacement()
    {
        var terrains = GameManager.TerrainGenerator.GetNextTerrains(MyX, GameManager.ENEMYBASE_RANGE);
        terrains[0].AddRobot(this);
        UpdateSpeed(terrains[0].TerrainType);

        for (int i = 0; i < terrains.Count; i++)
        {
            if (!terrains[i].HasEnemyBase)
                continue;
            if(i < GameManager.ENEMYBASE_RANGE)
                terrains[i].EnemyBase.OnBotEnteredMyRange(this);
            if (i < GameManager.ROBOT_RANGE)
                AquireTarget(terrains[i].EnemyBase);
        }
    }
    private void OnNewTerrainReached()
    {
        int newId = GameManager.TerrainGenerator.GetTerrainIDAtX(MyX);
        GameManager.TerrainGenerator.GetTerrainAtId(newId - 1).RemoveRobot(this);
        var terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId);
        terrain.AddRobot(this);
        Debug.Log("Reached " + terrain.TerrainType);
        UpdateSpeed(terrain.TerrainType);
        terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId + GameManager.ENEMYBASE_RANGE - 1);
        if (terrain.HasEnemyBase)
            terrain.EnemyBase.OnBotEnteredMyRange(this);
        terrain = GameManager.TerrainGenerator.GetTerrainAtId(newId + GameManager.ROBOT_RANGE - 1);
        if (terrain.HasEnemyBase)
            AquireTarget(terrain.EnemyBase);
    }

    public void AquireTarget(EnemyBase enemyBase)
    {
        targets.Enqueue(enemyBase);
        speed = 0;
    }
    public void OnTagetLost()
    {
        targets.Dequeue();
    }

    private void UpdateSpeed(TerrainType terrainType)
    {
        switch (terrainType)
        {
            case TerrainType.road:
                speed = 4;
                break;
            case TerrainType.grass:
                speed = 2;
                break;
            case TerrainType.mud:
                speed = 1;
                break;
        }
    }

}