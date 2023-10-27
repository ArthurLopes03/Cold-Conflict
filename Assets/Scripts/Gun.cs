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
    public KeyCode reload = KeyCode.R;

    [Header("Stats")]
    public float bulletSpeed;
    public float fireDelay;
    public GameObject ammoType;
    public int maxAmmo;
    private int ammoCount;
    public bool canShoot = true;

    [Header("Setup")]
    public GameObject instantiatePoint;
    public Animator gunAnimator;
    public TextMeshProUGUI textMesh;
    private string ammoString;
    public VisualEffect muzzleFlash;

    [Header("AudioSource")]
    public AudioSource fireSound;

    private void Start()
    {
        ammoCount = maxAmmo;
        SetAmmo();
    }
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && ammoCount != 0 && canShoot)
            Shoot();

        if(Input.GetKeyDown(aimKey))
        {
            gunAnimator.SetBool("ADS", true);
        }

        if (Input.GetKeyUp(aimKey))
        {
            gunAnimator.SetBool("ADS", false);
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
    }
}
