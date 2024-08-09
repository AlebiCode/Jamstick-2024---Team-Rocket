using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttacksKeys { PROJECTILES, EXPLOSIVES, ENERGY }
public enum DefencesKeys { ARMOR, FOAM, SHIELDS }
public enum MovementsKeys { PAWS, TRACKS, WHEELS }

[System.Serializable]
public class Loadout
{
    public AttacksKeys selectedAttack;
    public DefencesKeys selectedDefence;
    public MovementsKeys selectedMovement;


    public RobotWeapon GetAttackGameObject() 
    {
        return GameManager.PoolsManager.GetAttackGameObject(selectedAttack) as RobotWeapon;
    }
    public RobotArmor GetDefenceGameObject()
    {
        return GameManager.PoolsManager.GetDefenceGameObject(selectedDefence) as RobotArmor;
    }
    public RobotMovement GetMovementGameObject()
    {
        return GameManager.PoolsManager.GetMovementGameObject(selectedMovement) as RobotMovement;
    }
}
