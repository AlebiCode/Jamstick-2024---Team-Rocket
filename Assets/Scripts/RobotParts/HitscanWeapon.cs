using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private LineRenderer shotLine;

    private RaycastHit hitInfo;

    protected override void Attack()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 99, 1 << GameManager.GetTargetLayer(gameObject.layer), QueryTriggerInteraction.Collide))
        {
            hitInfo.transform.GetComponentInParent<Entity>().Damage(GameManager.BASE_DAMAGE, attackKey, transform.forward);
            shotLine.SetPosition(0, muzzlePosition.position);
            shotLine.SetPosition(1, hitInfo.point);
        }
    }

}
