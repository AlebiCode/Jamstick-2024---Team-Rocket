using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float DEATH_TIME = 5;

    [SerializeField] private AttacksKeys attackKey;
    [SerializeField] private float speed;
    private float length;

    private float deathTimer;

    private static RaycastHit hitInfo;
    
    public AttacksKeys AttacksKeys => attackKey;
    private float Damage => GameManager.BASE_DAMAGE;

    private void OnEnable()
    {
        deathTimer = DEATH_TIME;
    }

    // private IController controller
    private void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            Pool();
            return;
        }

        length = Time.deltaTime * speed;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, length, 1 << GetTargetLayer(gameObject.layer), QueryTriggerInteraction.Collide))
        {
            Impact();
            return;
        }
        transform.position += transform.forward * length;
    }

    public static int GetTargetLayer(int myLayer) 
    { 
        switch (myLayer) 
        {
            case 6:
                return 7;
            default: 
                return 6;
        }
    }
    private void Impact()
    {
        hitInfo.transform.GetComponentInParent<Entity>().Damage(Damage, attackKey, transform.forward);
        Pool();
    }
    private void Pool()
    {
        GameManager.PoolsManager.PoolBullet(this);
    }

    public static void GenerateBullet(Transform origin, AttacksKeys attackKey) 
    {
        GameObject bullet = GameManager.PoolsManager.GetBulletGameObject(attackKey);
        bullet.layer = origin.gameObject.layer;
        bullet.transform.position = origin.transform.position;
        bullet.transform.rotation = origin.rotation;
        bullet.SetActive(true);
    }

}
