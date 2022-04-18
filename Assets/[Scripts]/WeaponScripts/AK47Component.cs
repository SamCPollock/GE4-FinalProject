using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    Vector3 hitLocation;

    protected override void FireWeapon()
    {

        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;


                DealDamage(hit);


                Vector3 hiteDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hiteDirection.normalized * weaponStats.fireDistance, Color.red, 1.0f);
            }


        }

        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
            Debug.Log("AUTORELOADING");
        }

    }

    void DealDamage(RaycastHit hitInfo)
    {
        IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
        damageable?.TakeDamage(weaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.2f);
    }

}
