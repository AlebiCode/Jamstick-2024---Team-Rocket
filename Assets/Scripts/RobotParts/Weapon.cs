using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float reloadTime;
    [SerializeField] private AttacksKeys attackKey;
    [SerializeField] private bool isInRange;

    private float currentTime;
    private Transform target;

    public bool HasTarget => target != null;

    private void OnEnable()
    {
        currentTime = reloadTime;
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && isInRange) 
        {
            Bullet.GenerateBullet(transform, attackKey);
            currentTime = reloadTime;
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
        enabled = false;
    }

    public void SetInRange(bool isInRange) 
    {
        this.isInRange = isInRange;
    }
}
