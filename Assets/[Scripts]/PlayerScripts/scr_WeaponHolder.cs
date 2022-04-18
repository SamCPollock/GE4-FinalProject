using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr_WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]

    GameObject weaponToSpawn;

    public scr_PlayerController playerController;
    Animator animator; 
    
    Sprite crosshairImage;

    WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponSocket;

    [SerializeField]
    Transform gripIKSocketLocation;


    // animator hashes

    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");

    bool wasFiring = false;
    bool firingPressed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<scr_PlayerController>();

        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        scr_PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);

        gripIKSocketLocation= equippedWeapon.gripLocation; 
    }



    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
    }


    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }


    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
        }
        else
        {
            animator.SetBool(isFiringHash, true);
            playerController.isFiring = true;
            equippedWeapon.StartFiringWeapon();
        }

    }

    public void StopFiring()
    {
        animator.SetBool(isFiringHash, false);

        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();


    }

    // input based reload
    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;

        StartReloading();

    }
    //action of reload
    public void StartReloading()
    {
        equippedWeapon.weaponStats.bulletsInClip = equippedWeapon.weaponStats.clipSize;

        //if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInClip == equippedWeapon.weaponStats.clipSize) return;
        //if (equippedWeapon.weaponStats.bulletsInClip == 30)
        //{
        //    Debug.Log("FULL AMMO!");
        //    return;
        //}
        if (playerController.isFiring)
        {
            StopFiring();
        }

        if (equippedWeapon.weaponStats.totalBullets <= 0) return;

        animator.SetBool(isReloadingHash, true);

        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);

    }


    public void StopReloading()
    {
        if (animator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));

    }
}
