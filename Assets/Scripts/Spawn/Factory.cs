using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private int spawnNumber;
    public GameObject robotPrefab;
    private Vector3 offset = new Vector3(0, 0, 3);
    private Vector3 currentPosition = Vector3.zero;
    [SerializeField] List<GameObject> aliveRobots;

    private void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPosition = Vector3.zero;
            for(int i = 0; i < spawnNumber; i++) 
            { 
                GameObject robot = Instantiate(robotPrefab);
                robot.transform.position = currentPosition;
                aliveRobots.Add(robot);
                currentPosition += offset;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return)) 
        { 
            foreach(GameObject robot in aliveRobots) 
            { 
                Destroy(robot);
            }
        }

    }
}
