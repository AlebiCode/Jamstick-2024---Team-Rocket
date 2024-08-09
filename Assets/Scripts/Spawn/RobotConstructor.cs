using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConstructor
{
    private AttacksKeys selectedAttack;
    private DefencesKeys selectedDefence;
    private MovementsKeys selectedMovement;

    private RobotWeapon selectedAttackPart;
    private RobotArmor selectedDefencePart;
    private RobotMovement selectedMovementPart;

    public RobotWeapon SelectedAttackPart => selectedAttackPart;

    public void Spawn(Robot robot)
    {
        selectedAttack = GameManager.Factory.Loadout.selectedAttack;
        selectedDefence = GameManager.Factory.Loadout.selectedDefence;
        selectedMovement = GameManager.Factory.Loadout.selectedMovement;

        selectedAttackPart = GameManager.Factory.Loadout.GetAttackGameObject();
        selectedDefencePart = GameManager.Factory.Loadout.GetDefenceGameObject();
        selectedMovementPart = GameManager.Factory.Loadout.GetMovementGameObject();

        selectedDefencePart.transform.parent = robot.transform;
        selectedDefencePart.transform.localPosition = Vector3.zero;

        Transform attackSlot = selectedDefencePart.transform.Find("AttackSlot");
        Transform movementSlot = selectedDefencePart.transform.Find("MovementSlot");

        selectedAttackPart.transform.parent = attackSlot;
        selectedAttackPart.transform.localPosition = Vector3.zero;
        selectedAttackPart.gameObject.layer = robot.gameObject.layer;

        selectedMovementPart.transform.parent = movementSlot;
        selectedMovementPart.transform.localPosition = Vector3.zero;

        //CreateCollider();

        selectedAttackPart.gameObject.SetActive(true);
        selectedDefencePart.gameObject.SetActive(true);
        selectedMovementPart.gameObject.SetActive(true);
    }

    private void Despawn()
    {
        selectedAttackPart.transform.parent = null;
        selectedDefencePart.transform.parent = null;
        selectedMovementPart.transform.parent = null;

        selectedAttackPart.gameObject.SetActive(false);
        selectedDefencePart.gameObject.SetActive(false);
        selectedMovementPart.gameObject.SetActive(false);
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
