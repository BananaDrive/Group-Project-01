using NUnit;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Firearm : MonoBehaviour
{

    [Header("Weapon Stats")]
    public int weaponID;
    public float shotVel;
    public int fireMode;
    public float fireRate;
    public int currentClip;
    public int clipSize;
    public int maxAmmo;
    public int currentAmmo;
    public int reloadAmt;
    public float bulletLifespan;
    public float casingspeed;

    public int ShotgunBB;

    [Header("Weapon Library")]
    public bool useWeapon0 = true; // M1911
    public bool useWeapon1 = false; // M4
    public bool useWeapon2 = false; // FN SCAR
    public bool useWeapon3 = false;
    public bool useWeapon4 = false;
    public bool useWeapon5 = false;
    public bool CanFire = true;
    public Transform camera;


    [Header("Weapon Objects")]
    public GameObject shot;
    public GameObject muzzleFlashPrefab;
    public GameObject[] casingPrefabs;
    public PlayerBasicScript playerAmmo;
    public Transform gunTransform;


    [Header("Weapon Models")]
    public GameObject[] weapons;
    public GameObject[] weaponModels;
    public Transform weaponslot;
    public GameObject[] weaponpickups;
    private int currentWeaponIndex = -1;
    public bool[] weaponUnlocked;



    [Header("Weapon Locational Data")]
    public GameObject[] bulletInstantiators;
    public GameObject[] casingInstantiators;


    [Header("UI Elements")]
    public TextMeshProUGUI ammoText;
    private bool isReloading = false;


    private void Start()
    {
        weaponUnlocked = new bool[weapons.Length];
        for (int i = 0; i < weaponUnlocked.Length; i++)
        {
            weaponUnlocked[i] = false;
            weaponModels[i].SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons.Length > i && weaponUnlocked[i])
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    SwitchWeapon(i);
                }
            }
            else
            {
                Debug.Log($"Weapon {i} is locked.");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ShowAmmoPrompt();
        }
        else if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isReloading = false;
            ammoText.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0) && currentAmmo > 0 && CanFire)
        {
            if (currentClip > 0)
            {
                Debug.Log("Firing weapon...");
                Fire();
            }
            else
            {
                StartCoroutine(ReloadCoroutine());
            }

        }
    }

    public void SetupWeapon(int id)
    {
        switch (id)
        {
            case 0 when useWeapon0: // pistol
                weaponID = 0;
                shotVel = 50f;
                fireMode = 0;
                fireRate = 0.5f;
                currentClip = 7;
                clipSize = 7;
                maxAmmo = 14;
                currentAmmo = 21;
                reloadAmt = 7;
                bulletLifespan = 1.5f;
                break;

            case 1 when useWeapon1: //shotgun
                weaponID = 1;
                shotVel = 900f;
                fireMode = 1;
                fireRate = 0.083f;
                currentClip = 30;
                clipSize = 30;
                maxAmmo = 60;
                currentAmmo = 90;
                reloadAmt = 30;
                bulletLifespan = 2f;
                break;

            
            default:
                Debug.Log("Invalid weapon ID or weapon not available.");
                break;

        }
    }

    public void Fire()
    {
        if (Time.timeScale == 1)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, gunTransform.position, gunTransform.rotation);

            Transform cameraTransform = Camera.main.transform;
            float intensity = 2f;
            float duration = 0.5f;


            GameObject bulletInstantiator = bulletInstantiators[weaponID];
            GameObject projectile = Instantiate(shot, bulletInstantiator.transform.position, bulletInstantiator.transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(camera.transform.forward * shotVel, ForceMode.Impulse);
            }

            currentClip--;
            CanFire = false;

            Destroy(projectile, bulletLifespan);
            Destroy(muzzleFlash, 0.1f);

            StartCoroutine(CooldownFire());
            StartCoroutine(GunAction());
        }
    }

    private void ShowAmmoPrompt()
    {
        ammoText.text = $" {currentClip}";
        ammoText.gameObject.SetActive(true);
    }


    public void SwitchWeapon(int weaponIndex)
    {
        Debug.Log($"Attempting to switch to weapon index: {weaponIndex}");

        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponUnlocked[weaponIndex])
        {
            Debug.Log($"Switching to weapon: {weapons[weaponIndex].name}");

            if (currentWeaponIndex >= 0)
            {
                weapons[currentWeaponIndex].SetActive(false);
                weaponModels[currentWeaponIndex].SetActive(false);
            }

            weapons[weaponIndex].SetActive(true);
            weaponModels[weaponIndex].SetActive(true);
            currentWeaponIndex = weaponIndex;

            SetupWeapon(weaponIndex);
        }
        else
        {
            Debug.LogError("Cannot switch to weapon: either the index is invalid or the weapon is locked.");
        }
    }

    public void UnlockWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weaponUnlocked.Length)
        {
            weaponUnlocked[weaponIndex] = true;
            SwitchWeapon(weaponIndex);
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        ammoText.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        int reloadCount = clipSize - currentClip;
        int availableAmmo = playerAmmo.GetCurrentAmmo(weaponID);

        if (availableAmmo < reloadCount)
        {
            currentClip += availableAmmo;
            playerAmmo.DecreaseAmmo(weaponID, availableAmmo);
        }
        else
        {
            currentClip += reloadCount;
            playerAmmo.DecreaseAmmo(weaponID, reloadCount);
        }

        isReloading = false;
    }

    private IEnumerator CooldownFire()
    {
        yield return new WaitForSeconds(fireRate);
        CanFire = true;
    }

    IEnumerator GunAction()
    {
        yield return new WaitForSeconds(0.01f);
    }



   



}
