using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode aimKey = KeyCode.Mouse1;
    public KeyCode reload = KeyCode.R;

    [Header("Stats")]
    public float bulletSpeed;
    public float damage;
    public float fireDelay;
    public GameObject ammoType;
    public int maxAmmo;
    private int ammoCount;
    private bool canShoot = true;

    [Header("Setup")]
    public GameObject instantiatePoint;
    public GameObject shootDirection;

    private void Start()
    {
        ammoCount = maxAmmo;
    }
    void Update()
    {
        if (Input.GetKeyDown(shootKey) && ammoCount != 0 && canShoot)
            Shoot();
    }

    private void Shoot()
    {
        canShoot = false;
        ammoCount--;
        var instance = Instantiate(ammoType, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
        instance.GetComponent<Rigidbody>().MovePosition(shootDirection.transform.forward * bulletSpeed);
        Invoke(nameof(ResetShoot), fireDelay);
    }

    private void Reload()
    {
        ammoCount = maxAmmo;
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
