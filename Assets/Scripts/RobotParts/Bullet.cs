using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AttacksKeys attackKey;
    [SerializeField] private float speed;
    private float length;
    // private IController controller
    private void Update()
    {
        length = Time.deltaTime * speed;
        if (Physics.Raycast(transform.position, transform.forward, length, GetTargetLayer(gameObject.layer), QueryTriggerInteraction.Collide))
        {
            //Metodo per danno
            return;
        }
            transform.position += Vector3.right * length;
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

    public static void GenerateBullet(int layerMask, Vector3 startPosition, AttacksKeys attackKey) 
    {
        GameObject bullet = GameManager.PoolsManager.GetBulletGameObject(attackKey);
        bullet.layer = layerMask;
        bullet.transform.position = startPosition;
        bullet.SetActive(true);
    }
}
