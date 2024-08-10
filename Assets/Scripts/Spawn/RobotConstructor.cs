using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConstructor
{
    private RobotWeapon selectedAttackPart;
    private RobotArmor selectedDefencePart;
    private RobotMovement selectedMovementPart;

    public RobotWeapon SelectedAttackPart => selectedAttackPart;
    public RobotArmor SelectedDefencePart => selectedDefencePart;
    public RobotMovement SelectedMovementPart => selectedMovementPart;

    public void Spawn(Robot robot)
    {
        selectedAttackPart = GameManager.Factory.Loadout.GetAttackGameObject();
        selectedDefencePart = GameManager.Factory.Loadout.GetDefenceGameObject();
        selectedMovementPart = GameManager.Factory.Loadout.GetMovementGameObject();

        selectedAttackPart.gameObject.SetActive(true);
        selectedDefencePart.gameObject.SetActive(true);
        selectedMovementPart.gameObject.SetActive(true);

        selectedMovementPart.transform.SetParent(robot.transform);
        selectedMovementPart.gameObject.layer = 6;
        selectedMovementPart.transform.localPosition = new Vector3(0, selectedMovementPart.HalfHeight);
        //selectedMovementPart.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        selectedMovementPart.transform.rotation = Quaternion.Euler(Vector3.zero);

        selectedDefencePart.transform.SetParent(robot.transform);
        selectedDefencePart.gameObject.layer = 6;
        selectedDefencePart.transform.localPosition = new Vector3(0, selectedMovementPart.Height + selectedDefencePart.HalfHeight);
        //selectedDefencePart.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        selectedDefencePart.transform.rotation = Quaternion.Euler(Vector3.zero);

        selectedAttackPart.transform.SetParent(robot.transform);
        selectedAttackPart.gameObject.layer = 6;
        selectedAttackPart.transform.localPosition = new Vector3(0, selectedMovementPart.Height + selectedDefencePart.Height + selectedAttackPart.HalfHeight);
        selectedAttackPart.gameObject.layer = robot.gameObject.layer;
        //selectedAttackPart.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
        selectedAttackPart.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Despawn()
    {
        DisablePhysics();

        selectedAttackPart.transform.SetParent(GameManager.PoolsManager.PoolParent);
        selectedDefencePart.transform.SetParent(GameManager.PoolsManager.PoolParent);
        selectedMovementPart.transform.SetParent(GameManager.PoolsManager.PoolParent);

        selectedAttackPart.gameObject.SetActive(false);
        selectedDefencePart.gameObject.SetActive(false);
        selectedMovementPart.gameObject.SetActive(false);
    }
    public void EnablePhysics()
    {
        SelectedAttackPart.gameObject.layer = 0;
        SelectedAttackPart.Rigidbody.isKinematic = false;
        SelectedAttackPart.Collider.isTrigger = false;

        selectedDefencePart.gameObject.layer = 0;
        SelectedDefencePart.Rigidbody.isKinematic = false;
        SelectedDefencePart.Collider.isTrigger = false;

        SelectedMovementPart.gameObject.layer = 0;
        SelectedMovementPart.Rigidbody.isKinematic = false;
        SelectedMovementPart.Collider.isTrigger = false;
    }
    private void DisablePhysics()
    {
        SelectedAttackPart.Rigidbody.isKinematic = true;
        SelectedAttackPart.Collider.isTrigger = true;

        SelectedDefencePart.Rigidbody.isKinematic = true;
        SelectedDefencePart.Collider.isTrigger = true;

        SelectedMovementPart.Rigidbody.isKinematic = true;
        SelectedMovementPart.Collider.isTrigger = true;
    }


    /*
    private void CreateCollider() 
    { 
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();

        float colliderHeight = selectedAttackPart.GetComponent<RobotPart>().Height + selectedDefencePart.GetComponent<RobotPart>().Height + selectedMovementPart.GetComponent<RobotPart>().Height;

        Vector3 colliderSize = new Vector3(1, colliderHeight, 1);
        
        collider.isTrigger = true;
        collider.size = colliderSize;
    }
    */
}
