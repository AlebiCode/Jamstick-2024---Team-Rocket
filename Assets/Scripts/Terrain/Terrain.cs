using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TerrainType { grass, mud, road, ENUM_LENGHT }

public class Terrain : MonoBehaviour
{
    [SerializeField] private UnityEvent<Robot> onBotEnter;
    [SerializeField] private TerrainType type;

    public UnityEvent<Robot> OnBotEnter => onBotEnter;
    public TerrainType TerrainType => type;

    public void ResetMe()
    {
        OnBotEnter.RemoveAllListeners();
    }

}
