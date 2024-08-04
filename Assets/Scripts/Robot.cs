using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] private float speed = 4;

    public float terrainAdvancement;

    private float MyX => transform.position.x;

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
        CameraController.instance.SetTargetX(MyX);
    }
    
    private void Activate()
    {
        terrainAdvancement = (MyX % TerrainGenerator.TERRAIN_WIDTH) * TerrainGenerator.TERRAIN_WIDTH;
        OnNewTerrainReached();
    }
    private void OnNewTerrainReached()
    {
        Terrain terrain = TerrainGenerator.instance.GetTerrainAtX(MyX);
        UpdateSpeed(terrain.TerrainType);
        terrain.OnBotEnter.Invoke(this);
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
