using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotConstructor : MonoBehaviour
{
    private AttacksKeys selectedAttack;
    private DefencesKeys selectedDefence;
    private MovementsKeys selectedMovement;

    private GameObject selectedAttackPart;
    private GameObject selectedDefencePart;
    private GameObject selectedMovementPart;

    private void Awake()
    {
        Spawn();
    }

    private void OnDestroy()
    {
        Despawn();
    }

    private void Spawn()
    {
        selectedAttack = Loadout.Instance.selectedAttack;
        selectedDefence = Loadout.Instance.selectedDefence;
        selectedMovement = Loadout.Instance.selectedMovement;

        selectedAttackPart = Loadout.Instance.GetAttackGameObject();
        selectedDefencePart = Loadout.Instance.GetDefenceGameObject();
        selectedMovementPart = Loadout.Instance.GetMovementGameObject();

        selectedDefencePart.transform.parent = transform;
        selectedDefencePart.transform.localPosition = Vector3.zero;

        Transform attackSlot = selectedDefencePart.transform.Find("AttackSlot");
        Transform movementSlot = selectedDefencePart.transform.Find("MovementSlot");

        selectedAttackPart.transform.parent = attackSlot;
        selectedAttackPart.transform.localPosition = Vector3.zero;

        selectedMovementPart.transform.parent = movementSlot;
        selectedMovementPart.transform.localPosition = Vector3.zero;

        selectedAttackPart.SetActive(true);
        selectedDefencePart.SetActive(true);
        selectedMovementPart.SetActive(true);
    }

    private void Despawn()
    {
        selectedAttackPart.transform.parent = null;
        selectedDefencePart.transform.parent = null;
        selectedMovementPart.transform.parent = null;

        selectedAttackPart.SetActive(false);
        selectedDefencePart.SetActive(false);
        selectedMovementPart.SetActive(false);
    }
}
