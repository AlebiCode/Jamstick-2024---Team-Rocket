using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    [SerializeField] Transform aimAtMePoint;
    [SerializeField] private float hp = 10;
    

    private UnityEvent onDeath = new UnityEvent();

    protected bool IsAlive => hp > 0;
    public UnityEvent OnDeath => onDeath;
    public Transform AimAtMePoint => aimAtMePoint;

    public void SetTraget(Transform target)
    {
        this.aimAtMePoint = target;
    }

    public virtual void Damage(float damage, AttacksKeys key, Vector3 dir)
    {
        if (hp <= 0)
            return;

        damage = ApplyDamageModifiers(damage, key);
        hp -= damage;
        if (hp <= 0)
            Death();

        RecieveForce(dir);
    }
    public void Kill()
    {
        Damage(hp, AttacksKeys.TRUE, Vector3.down);
    }

    protected virtual float ApplyDamageModifiers(float damage, AttacksKeys key)
    {
        return damage;
    }

    protected virtual void RecieveForce(Vector3 dir)
    {

    }

    protected virtual void Death()
    {
        onDeath.Invoke();
        onDeath.RemoveAllListeners();
    }

}
