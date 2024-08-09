using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float reloadTime;
    [SerializeField] private AttacksKeys attackKey;
    private float currentTime;
    [SerializeField]private bool isInRange;

    private void OnEnable()
    {
        currentTime = reloadTime;
    }
    private void Update()
    {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && isInRange) 
            {
                Bullet.GenerateBullet(gameObject.layer, transform.position, attackKey);
                currentTime = reloadTime;
            }
    }

    public void SetInRange(bool isInRange) 
    {
        this.isInRange = isInRange;
    }
}
