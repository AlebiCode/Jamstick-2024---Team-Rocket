using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    private List<Terrain> myTerrainsRange;
    private Robot currentTarget;


    public void Update()
    {
        if (!currentTarget.IsAlive)
        {
            enabled = false;
            return;
        }
        Shoot();
    }

    public void Initialize(Terrain terrain)
    {
        myTerrainsRange = GameManager.TerrainGenerator.GetPreviousTerrains(terrain);
        TargetClosestRobot();
    }

    public void OnBotEnteredMyRange(Robot robot)
    {
        if (currentTarget != null)
            return;
        if (currentTarget == null)
            TargetClosestRobot();
    }

    private void TargetClosestRobot()   //also call when target robot dies!!
    {
        /*
        for (int i = myTerrainsRange.Count - 1; i >= 0; i++)
        {
            Robot r = myTerrainsRange[i].GetFurthestRobot();
            if (r != null)
            {
                currentTarget = r;
                enabled = true;
                return;
            }
        }
        enabled = false;
        */
    }

    private void Shoot()
    {

    }

}
