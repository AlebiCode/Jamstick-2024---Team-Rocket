using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Factory : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Robot robotPrefab;
    [SerializeField] private Animator head;
    [SerializeField] private int spawnNumber;
    [SerializeField] private float spawnCoolDown;
    [SerializeField] private Loadout loadout = new Loadout();

    [SerializeField] private TMP_Text productionButtonText;

    private float zOffset = 3;

    private float timer;
    private bool spawnZigZag;

    public Loadout Loadout => loadout;

    private void OnDisable()
    {
        timer = 0;
    }
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
        float currentZOffset = - (spawnNumber - (spawnZigZag ? 0.5f : 1.5f)) * zOffset / 2;
        spawnZigZag = !spawnZigZag;
        int toSpawn = GameManager.instance.TryBuyRobots(spawnNumber);
        List<Robot> spawned = new List<Robot>();
        for (int i = 0; i < toSpawn; i++)
        {
            Robot robot = Instantiate(robotPrefab, parent);
            robot.transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, currentZOffset);
            robot.enabled = false;
            currentZOffset += zOffset;
            spawned.Add(robot);
            GameManager.instance.OnRobotSpawn(robot);
            robot.Initialize();
        }
        if(toSpawn > 0)
            StartCoroutine(TransportCoroutine(spawned));
    }

    private IEnumerator TransportCoroutine(List<Robot> toMove)
    {
        float xSpeed = 10;
        float current = 0;
        while (toMove[0].transform.position.y > 0)
        {
            current -= 9.81f * Time.deltaTime;
            foreach (Robot robot in toMove)
                robot.transform.position += new Vector3(xSpeed, current) * Time.deltaTime;
            yield return null;
        }
        foreach (Robot robot in toMove)
        {
            robot.transform.position = new Vector3(robot.transform.position.x, 0, robot.transform.position.z);
            robot.Activate();
        }
    }

    public void ToggleActivation()
    {
        SetActivation(!enabled);
    }
    public void SetActivation(bool value)
    {
        enabled = value;
        productionButtonText.text = value ? "Stop Production" : "Start Production";
        productionButtonText.color = value ? Color.red : Color.green;
        head.SetBool("Active", value);
    }
    public void SetAttack(int keyIndex)
    {
        loadout.SetAttack(keyIndex);
    }
    public void SetDefence(int keyIndex)
    {
        loadout.SetDefence(keyIndex);
    }
    public void SetMovement(int keyIndex)
    {
        loadout.SetMovement(keyIndex);
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