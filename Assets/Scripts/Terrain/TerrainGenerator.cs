using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TerrainGenerator : MonoBehaviour
{
    public const float TERRAIN_WIDTH = 4;

    private const string PATH_PREFABS_TERRAINS = "Prefabs/Terrain/Variants";
    
    [SerializeField] private int roadLenght = 5;
    [SerializeField] private EnemyBase enemyBasePrefab;

    private List<Terrain>[] readyTerrains;
    private List<Terrain> activeTerrains = new List<Terrain>();

    private Terrain OldestTerrain => activeTerrains[0];
    private Terrain NewestTerrain => activeTerrains[activeTerrains.Count - 1];
    private Vector3 NextPosition => activeTerrains.Count > 0 ? NewestTerrain.transform.position + Vector3.right * TERRAIN_WIDTH : Vector3.zero;
    public EnemyBase EnemyBasePrefab => enemyBasePrefab;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        LoadTerrains();
        PlaceFirstTerrains();
    }
    public void Reset()
    {
        for (int i = 0; i < activeTerrains.Count; i++)
            Destroy(activeTerrains[i].gameObject);
        activeTerrains.Clear();
        PlaceFirstTerrains();
    }

    private void LoadTerrains()
    {
        Transform[] terrainParents = new Transform[(int)TerrainType.ENUM_LENGHT];
        for (int i = 0; i < terrainParents.Length; i++)
        {
            terrainParents[i] = new GameObject().transform;
            terrainParents[i].SetParent(transform);
            terrainParents[i].name = ((TerrainType)i).ToString() + "_Terrains";
        }

        readyTerrains = new List<Terrain>[(int)TerrainType.ENUM_LENGHT];
        for (int i = 0; i < readyTerrains.Length; i++)
            readyTerrains[i] = new List<Terrain>();

        var allTerrains = Resources.LoadAll<Terrain>(PATH_PREFABS_TERRAINS);
        foreach (Terrain t in allTerrains)
        {
            for (int i = 0; i < roadLenght; i++)    //sbagliato! è un fix lezzo per essere sicuro di aver abbastanza pezzi id strada
            {
                Terrain instance = Instantiate(t, terrainParents[(int)t.TerrainType]);
                readyTerrains[(int)instance.TerrainType].Add(instance);
                instance.gameObject.SetActive(false);
            }
        }
    }
    private void PlaceFirstTerrains()
    {
        for (int i = 0; i < roadLenght; i++)
            AddRandomTypeTerrain();
    }

    public void AddRandomTypeTerrain()
    {
        AddTerrain((TerrainType)UnityEngine.Random.Range(0, (int)TerrainType.ENUM_LENGHT));
    }
    private void AddTerrain(TerrainType terrainType)
    {
        if (activeTerrains.Count >= roadLenght)
            RemoveTerrain();

        var newTerrain = RandomRetrieveFromReadied(terrainType);

        newTerrain.transform.position = NextPosition;
        newTerrain.gameObject.SetActive(true);
        newTerrain.PositionId = activeTerrains.Count;
        activeTerrains.Add(newTerrain);

        if (UnityEngine.Random.Range(0,3) == 0)
            newTerrain.AddEnemyBase();
    }
    private void RemoveTerrain()
    {
        var toRemove = activeTerrains[0];
        activeTerrains.RemoveAt(0);
        readyTerrains[(int)toRemove.TerrainType].Add(toRemove);
        toRemove.ResetMe();
        toRemove.gameObject.SetActive(false);
        for (int i = 0; i < activeTerrains.Count; i++)
            activeTerrains[i].PositionId--;
    }
    private Terrain RandomRetrieveFromReadied(TerrainType terrainType)
    {
        int readiedIndex = UnityEngine.Random.Range(0, readyTerrains[(int)terrainType].Count);
        Terrain newTerrain = readyTerrains[(int)terrainType][readiedIndex];
        readyTerrains[(int)terrainType].RemoveAt(readiedIndex);
        return newTerrain;
    }

    public void RendererPositionUpdate(float x)
    {
        if (x >= activeTerrains[roadLenght/2].transform.position.x)
            AddRandomTypeTerrain();
    }
    public Terrain GetTerrainAtX(float x)
    {
        return activeTerrains[GetTerrainIDAtX(x)];
    }
    public int GetTerrainIDAtX(float x)
    {
        return (int)((x - OldestTerrain.transform.position.x) / TERRAIN_WIDTH);
    }

    public Terrain GetTerrainAtId(int id)
    {
        return activeTerrains[id];
    }
    public List<Terrain> GetTerrainAtX(float x, int count)
    {
        return activeTerrains.GetRange((int)((x - OldestTerrain.transform.position.x) / TERRAIN_WIDTH), count);
    }
    public List<Terrain> GetPreviousTerrains(Terrain terrain)
    {
        for (int i = 0; i < activeTerrains.Count; i++)
        {
            if (activeTerrains[i] == terrain)
                return activeTerrains.GetRange(0, i + 1);
        }
        return new List<Terrain>();
    }

    public List<Terrain> GetNextTerrains(float xPos, int count)
    {
        return activeTerrains.GetRange(GetTerrainIDAtX(xPos), count);
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

        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if (GUILayout.Button("Add Random Terrain"))
            TerrainGenerator.AddRandomTypeTerrain();
        if (GUILayout.Button("Reset"))
            TerrainGenerator.Reset();
        EditorGUI.EndDisabledGroup();

    }
}