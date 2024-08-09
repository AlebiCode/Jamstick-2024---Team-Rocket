using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public const int ROBOT_RANGE = 2;
    public const int ENEMYBASE_RANGE = 3;


    public static GameManager instance;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private TerrainGenerator terrainGenerator;
    [SerializeField] private int money = 1000;

    private List<Robot> aliveRobots = new List<Robot>();

    public static CameraController CameraController => instance.cameraController;
    public static TerrainGenerator TerrainGenerator => instance.terrainGenerator;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void OnRobotSpawn(Robot robot)
    {
        aliveRobots.Add(robot);
    }
    public void OnRobotDeath(Robot robot)
    {
        aliveRobots.Remove(robot);
    }

    public void Advancement(float x)
    {
        cameraController.SetTargetX(x);
    }

}
