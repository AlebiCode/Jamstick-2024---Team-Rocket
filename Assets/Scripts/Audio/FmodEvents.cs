using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour
{
    [field: Header("Robots")]
    [field: SerializeField] public EventReference RobotDeath { get; private set; }

    [field: Header("Humans")]
    [field: SerializeField] public EventReference HumanDeath { get; private set; }

    [field: Header("Weapons")]
    [field: SerializeField] public EventReference ProjectileShoot { get; private set; }
    [field: SerializeField] public EventReference ExplosiveShoot { get; private set; }
    [field: SerializeField] public EventReference EnergyShoot { get; private set; }

    [field: Header("Factory")]
    [field: SerializeField] public EventReference FactoryMoving { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference MainMenuMusic { get; private set; }
    [field: SerializeField] public EventReference InGameMusic { get; private set; }
    [field: SerializeField] public EventReference ChillMusic { get; private set; }
    [field: SerializeField] public EventReference CreditsMusic { get; private set; }

    public static FmodEvents Instance { get; private set; }

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
}
