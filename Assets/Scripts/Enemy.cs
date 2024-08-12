using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    [SerializeField] private Transform weaponSocket;
    [SerializeField] private Collider myHitCollider;
    [SerializeField] private RagdollUser ragdollUser;
    [SerializeField] private DefencesKeys myDefence;


    private Weapon weapon;
    
    public Weapon Weapon => weapon;
    public bool HasTarget => weapon.HasTarget;

    public void Initialize(Weapon weapon)
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
        this.weapon = weapon;
        weapon.transform.SetParent(weaponSocket, false);
        //weapon.transform.localPosition = Vector3.zero;
        weapon.gameObject.layer = gameObject.layer;
    }

    public void FindTarget(Terrain terrain)
    {
        if (!IsAlive)
            return;
        for (int i = terrain.myPositionId; i >= 0; i--)
        {
            Terrain t = GameManager.TerrainGenerator.GetTerrainAtId(i);
            if (t.HasBots)
            {
                Robot r = t.GetClosestRobotToZ(transform.position.z);
                weapon.EngageTarget(r);
                r.OnDeath.AddListener(() => FindTarget(terrain));
                return;
            }
        }
        weapon.Disengage();
    }

    protected override void RecieveForce(Vector3 dir)
    {
        //rb.AddForce(dir, ForceMode.Impulse);
    }

    protected override float ApplyDamageModifiers(float damage, AttacksKeys atkKey)
    {
        return damage * GameManager.GetDamageMultiplier(atkKey, myDefence);
    }
    protected override void Death()
    {
        base.Death();
        gameObject.layer = 0;
        Weapon.gameObject.layer = 0;
        Weapon.Disengage();
        Weapon.transform.SetParent(null);
        var col = Weapon.transform.GetComponent<Collider>();
        col.enabled = true;
        col.attachedRigidbody.isKinematic = false;
        ragdollUser.SetActivation(true);
        ragdollUser.Impulse(new Vector3(1, Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f)));
        myHitCollider.enabled = false;
        GameManager.instance.AddMoney(GameManager.SINGLE_ENEMY_LOOT);
        StartCoroutine(DestroyCo());
    }

    private IEnumerator DestroyCo()
    {
        yield return new WaitForSeconds(5);
        Destroy(Weapon.gameObject);
        Destroy(gameObject);
    }
    
}
