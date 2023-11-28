using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode aimKey = KeyCode.Mouse1;
    public KeyCode reloadKey = KeyCode.R;

    [Header("Stats")]
    public float bulletSpeed;
    public GameObject ammoType;
    public int maxAmmo;
    private int ammoCount;
    public bool canShoot = true;
    public bool isAutomatic;
    public float maxSpread = 5;

    [Header("Setup")]
    public GameObject instantiatePoint;
    public Animator gunAnimator;
    public TextMeshProUGUI textMesh;
    private string ammoString;
    public VisualEffect muzzleFlash;
    public bool isReloading = false;
    public bool isAiming = false;
    public PlayerMovement playerMovement;

    [Header("AudioSource")]
    public AudioSource fireSound;
    public AudioSource emptySound;
    public AudioSource[] reloadSounds;

    private void Start()
    {
        ammoCount = maxAmmo;
        SetAmmo();
    }
    void Update()
    {
        //If the player is sprinting, the run animation plays
        if (playerMovement.state == PlayerMovement.MovementState.sprinting && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            gunAnimator.SetBool("Running", true);
        }

        else
        {
            gunAnimator.SetBool("Running", false);
        }

        //If the gun has no ammo, it plays a click instead
        if (Input.GetKeyDown(shootKey) && ammoCount <= 0 && playerMovement.state != PlayerMovement.MovementState.sprinting)
            PlayClick();

        //If the gun is not automatic, it will only fire once per click
        if (Input.GetKeyDown(shootKey) && ammoCount != 0 && canShoot && !isAutomatic && playerMovement.state != PlayerMovement.MovementState.sprinting)
            Shoot();

        //If the gun is automatic, as long as the player holds down the fire button, it will shoot
        if (Input.GetKey(shootKey) && ammoCount != 0 && canShoot && isAutomatic && playerMovement.state != PlayerMovement.MovementState.sprinting)
            Shoot();

        //When the player holds down the aim key, it will switch the animation mode to the ADS mode
        if (Input.GetKeyDown(aimKey))
        {
            isAiming = true;
            gunAnimator.SetBool("ADS", true);
            maxSpread = maxSpread / 2;
        }

        //When the player stops pushing the aim key, it will switch back to the normal mode
        if (Input.GetKeyUp(aimKey) && playerMovement.state != PlayerMovement.MovementState.sprinting)
        {
            isAiming = false;
            gunAnimator.SetBool("ADS", false);
            maxSpread = maxSpread * 2;
        }

        if(Input.GetKeyDown(reloadKey) && ammoCount != maxAmmo && !isReloading)
        {
            isReloading = true;
            gunAnimator.SetTrigger("Reload");
        }
    }

    private void Shoot()
    {
        canShoot = false;
        ammoCount--;
        Vector3 dir = instantiatePoint.transform.forward + new Vector3(Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread), Random.Range(-maxSpread, maxSpread));
        var instance = Instantiate(ammoType, instantiatePoint.transform.position, instantiatePoint.transform.rotation * Quaternion.AngleAxis(90, Vector3.right));
        instance.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed, ForceMode.Impulse);
        gunAnimator.SetTrigger("Fire");
        PlayFire();
        muzzleFlash.Play();
        //Invoke(nameof(ResetShoot), fireDelay);
        SetAmmo();
    }

    private void Reload()
    {
        ammoCount = maxAmmo;
        SetAmmo();
    }

    public void ResetShoot()
    {
        canShoot = true;
    }

    private void SetAmmo()
    {
        ammoString = ammoCount.ToString() + " / " + maxAmmo.ToString();
        textMesh.SetText(ammoString);
        isReloading = false;
    }

    private void PlayFire()
    {
        fireSound.Play();
    }

    private void PlayClick()
    {
        emptySound.Play();
    }

    private void PlayReloadSound(int i)
    {
        reloadSounds[i].Play();
    }
}
