using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TerrainType { grass, mud, road, ENUM_LENGHT }

public class Terrain : MonoBehaviour
{
    [SerializeField] private TerrainType type;
}
