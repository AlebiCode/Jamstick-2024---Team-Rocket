using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TerrainType { grass, mud, road, ENUM_LENGHT }

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainType type;
    [SerializeField] private Transform enemyBaseSocket;

    private int myPositionId;
    private EnemyBase enemyBase;
    public List<Robot> robots = new List<Robot>();

    public bool HasEnemyBase => enemyBase;
    public EnemyBase EnemyBase => enemyBase;
    public int PositionId { set { myPositionId = value; } get { return myPositionId; } }

    public TerrainType TerrainType => type;

    public void ResetMe()
    {
        robots.Clear();
    }

    public void AddEnemyBase()
    {
        enemyBase = Instantiate(GameManager.TerrainGenerator.EnemyBasePrefab, enemyBaseSocket, true);
        enemyBase.transform.localPosition = Vector3.zero;
        enemyBase.Initialize(this);
    }

    public void AddRobot(Robot robot)
    {
        robots.Add(robot);
    }
    public void RemoveRobot(Robot robot)
    {
        robots.Remove(robot);
    }


    public Robot GetFurthestRobot()
    {
        int furthestId = -1;
        float dist = float.NegativeInfinity;
        for (int i = 0; i < robots.Count; i++)
        {
            if (robots[i].MyX <= dist)
                continue;
            dist = robots[i].MyX;
            furthestId = i;
        }
        return furthestId >= 0 ? robots[furthestId] : null;
    }

}
