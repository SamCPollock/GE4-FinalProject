using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None, 
    Pistol, 
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;

    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;

    public WeaponFiringPattern weaponFiringPattern;

}

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public Transform firingEffectLocation;

    protected scr_WeaponHolder weaponHolder;
    [SerializeField] protected ParticleSystem firingEffect;


    [SerializeField]
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    public Camera mainCamera;



    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

    }

    public void Initialize(scr_WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;

    }

    // decide whether rapid fire or not. 
    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));

        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }

    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletsInClip--;
    }

    // deal with ammo counts and maybe particle effects. 
    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        isReloading = false;

    }

    protected virtual void ReloadWeapon()
    {
        // if there's a firing effect, hide it here

        int bulletsToReload = weaponStats.totalBullets - (weaponStats.clipSize - weaponStats.bulletsInClip);
        // -------------- COD style reload, subtract bullets ----------------------
        if (bulletsToReload > 0)
        {
            weaponStats.totalBullets = bulletsToReload;
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
        else

        {
            weaponStats.bulletsInClip += weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }
    }
}
