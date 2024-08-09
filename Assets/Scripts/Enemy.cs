using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    public Weapon Weapon => weapon;

    public void Initialize(Weapon weapon)
    {

    }

}
