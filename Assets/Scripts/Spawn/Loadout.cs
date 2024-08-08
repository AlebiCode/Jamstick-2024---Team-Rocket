using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttacksKeys { PROJECTILES, EXPLOSIVES, ENERGY }
public enum DefencesKeys { ARMOR, FOAM, SHIELDS }
public enum MovementsKeys { PAWS, TRACKS, WHEELS }
public class Loadout : MonoBehaviour
{
    public AttacksKeys selectedAttack;
    public DefencesKeys selectedDefence;
    public MovementsKeys selectedMovement;

    public static Loadout Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }



    public GameObject GetAttackGameObject() 
    {
        return PoolsManager.Instance.GetAttackGameObject(selectedAttack);
    }
    public GameObject GetDefenceGameObject()
    {
        return PoolsManager.Instance.GetDefenceGameObject(selectedDefence);
    }
    public GameObject GetMovementGameObject()
    {
        return PoolsManager.Instance.GetMovementGameObject(selectedMovement);
    }
}
