using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected AttacksKeys attackKey;
    [SerializeField] protected Transform muzzlePosition;

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
        if (currentTime <= 0) 
        {
            AimAtTarget();
            Attack();
            currentTime = ReloadTime;
        }
    }

    protected virtual void Attack()
    {
        Bullet.GenerateBullet(muzzlePosition.transform, attackKey);
    }

    public void EngageTarget(Entity target)
    {
        enabled = true;
        this.target = target.AimAtMePoint;
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

}
