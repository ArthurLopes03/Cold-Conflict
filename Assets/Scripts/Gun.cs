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

    [Header("Setup")]
    public GameObject instantiatePoint;
    public Animator gunAnimator;
    public TextMeshProUGUI textMesh;
    private string ammoString;
    public VisualEffect muzzleFlash;
    private bool isReloading = false;
    public PlayerMovement playerMovement;

    [Header("AudioSource")]
    public AudioSource fireSound;

    private void Start()
    {
        ammoCount = maxAmmo;
        SetAmmo();
    }
    void Update()
    {
        if (playerMovement.state == PlayerMovement.MovementState.sprinting)
        {
            gunAnimator.SetBool("Running", true);
        }

        else
        {
            gunAnimator.SetBool("Running", false);
        }

        if (Input.GetKeyDown(shootKey) && ammoCount != 0 && canShoot && !isAutomatic && playerMovement.state != PlayerMovement.MovementState.sprinting)
            Shoot();

        if (Input.GetKey(shootKey) && ammoCount != 0 && canShoot && isAutomatic && playerMovement.state != PlayerMovement.MovementState.sprinting)
            Shoot();

        if (Input.GetKeyDown(aimKey) && playerMovement.state != PlayerMovement.MovementState.sprinting)
        {
            gunAnimator.SetBool("ADS", true);
        }

        if (Input.GetKeyUp(aimKey) && playerMovement.state != PlayerMovement.MovementState.sprinting)
        {
            gunAnimator.SetBool("ADS", false);
        }

        if(Input.GetKeyDown(reloadKey) && ammoCount != maxAmmo && !isReloading && playerMovement.state != PlayerMovement.MovementState.sprinting)
        {
            gunAnimator.SetTrigger("Reload");
            isReloading = true;
        }
    }

    private void Shoot()
    {
        canShoot = false;
        ammoCount--;
        var instance = Instantiate(ammoType, instantiatePoint.transform.position, instantiatePoint.transform.rotation * Quaternion.AngleAxis(90, Vector3.right));
        instance.GetComponent<Rigidbody>().AddForce(instantiatePoint.transform.forward * bulletSpeed, ForceMode.Impulse);
        gunAnimator.SetTrigger("Fire");
        fireSound.Play();
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
        Debug.Log("Shot Reset");
        canShoot = true;
    }

    private void SetAmmo()
    {
        ammoString = ammoCount.ToString() + " / " + maxAmmo.ToString();
        textMesh.SetText(ammoString);
        isReloading = false;
    }
}
