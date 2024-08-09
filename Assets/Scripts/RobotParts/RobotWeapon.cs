using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotWeapon : RobotPart
{
    [SerializeField] private Weapon weapon;

    public Weapon Weapon => weapon;

}
