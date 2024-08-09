using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private int spawnNumber;
    [SerializeField] private float spawnCoolDown;
    [SerializeField] private Vector3 spawnPosOffset;
    [SerializeField] private Loadout loadout = new Loadout();

    private Vector3 zOffset = new Vector3(0, 0, 3);

    private float timer;

    public Loadout Loadout => loadout;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCoolDown)
        {
            Spawn();
            timer = 0;
        }
    }

    public void Spawn()
    {
        Vector3 currentZOffset = Vector3.zero;
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject robot = Instantiate(robotPrefab, parent);
            robot.transform.position = transform.position + spawnPosOffset + currentZOffset;
            currentZOffset += zOffset;
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
        GUILayout.Space(16);
        if (GUILayout.Button("Spawn"))
        {
            Factory.Spawn();
        }
        if (GUILayout.Button("Destroy"))
        {
            GameManager.instance.DestroyAllRobots();
        }
    }

}