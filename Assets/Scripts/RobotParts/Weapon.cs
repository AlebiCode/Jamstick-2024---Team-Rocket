using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AttacksKeys attackKey;
    [SerializeField] private bool isInRange;

    private float currentTime;
    public Transform target;

    private float ReloadTime => GameManager.FIRE_RATE;
    public bool HasTarget => target != null;

    private void OnEnable()
    {
        currentTime = ReloadTime;
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && isInRange) 
        {
            AimAtTarget();
            Bullet.GenerateBullet(transform, attackKey);
            currentTime = ReloadTime;
        }
    }

    public void EngageTarget(Transform target)
    {
        enabled = true;
        this.target = target;
        AimAtTarget();
    }
    public void AimAtTarget()
    {
        transform.LookAt(target, Vector3.up);
    }
    public void Disengage()
    {
        target = null;
        enabled = false;
    }

    public void SetInRange(bool isInRange) 
    {
        this.isInRange = isInRange;
    }
}
