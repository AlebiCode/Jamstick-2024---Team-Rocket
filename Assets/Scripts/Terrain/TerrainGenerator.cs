using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private Terrain grassTerrain;
    [SerializeField] private Terrain mudTerrain;
    [SerializeField] private Terrain roadTerrain;
    [SerializeField] private int roadLenght = 5;

    private Queue<Terrain> activeTerrains = new Queue<Terrain>();
    private Vector3 nextPosition;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for(int i = 0; i < roadLenght; i++)
            AddRandomTerrain();
    }
    public void Reset()
    {
        nextPosition = Vector3.zero;
        for (int i = 0; i < activeTerrains.Count; i++)
            Destroy(activeTerrains.Dequeue().gameObject);
        Initialize();
    }

    public void AddRandomTerrain()
    {
        AddTerrain((TerrainType)UnityEngine.Random.Range(0, (int)TerrainType.ENUM_LENGHT));
    }
    private void AddTerrain(TerrainType terrainType)
    {
        var newTerrain = Instantiate(GetTerrainFromType(terrainType));
        activeTerrains.Enqueue(newTerrain);

        newTerrain.transform.position = nextPosition;
        nextPosition += Vector3.right * newTerrain.transform.localScale.x;

        if (activeTerrains.Count > roadLenght)
            Destroy(activeTerrains.Dequeue().gameObject);
    }

    private Terrain GetTerrainFromType(TerrainType terrainType)
    {
        switch (terrainType) {
            case TerrainType.grass: return grassTerrain;
            case TerrainType.mud: return mudTerrain;
            case TerrainType.road: return roadTerrain;
        }
        return null;
    }

}

[CustomEditor(typeof(TerrainGenerator))]
[CanEditMultipleObjects]
public class TerrainGenerator_Inspector : Editor
{
    private TerrainGenerator TerrainGenerator => (TerrainGenerator)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(16);
        if (GUILayout.Button("Add Random Terrain"))
            TerrainGenerator.AddRandomTerrain();
        if (GUILayout.Button("Reset"))
            TerrainGenerator.Reset();
    }
}