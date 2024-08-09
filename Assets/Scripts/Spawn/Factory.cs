using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private int spawnNumber;
    public GameObject robotPrefab;
    private Vector3 offset = new Vector3(0, 0, 3);
    private Vector3 currentPosition = Vector3.zero;
    [SerializeField] private Transform parent;
    [SerializeField] List<GameObject> aliveRobots;

    private void Start()
    {
    }

    public void Spawn()
    {
        currentPosition = Vector3.zero;
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject robot = Instantiate(robotPrefab, parent);
            robot.transform.position = currentPosition;
            aliveRobots.Add(robot);
            currentPosition += offset;
        }
    }

    public void Destroy()
    {
        foreach (GameObject robot in aliveRobots)
        {
            Destroy(robot);
        }
    }

}

[CustomEditor(typeof(Factory))]
[CanEditMultipleObjects]
public class Factory_Editor : Editor
{

    private Factory Factory => (Factory)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Spawn"))
        {
            Factory.Spawn();
        }
        if (GUILayout.Button("Destroy"))
        {
            Factory.Destroy();
        }
    }

}