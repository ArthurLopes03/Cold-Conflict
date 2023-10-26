using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float existanceTime = 10f;
    public float damage;

    private void Start()
    {
        Invoke(nameof(RemoveBullet), existanceTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "HitBox")
        {
            collision.gameObject.GetComponentInParent<EnemyManager>().health -= collision.gameObject.GetComponent<HitBox>().hitModifier * damage;
            Debug.Log("Hit for " + collision.gameObject.GetComponent<HitBox>().hitModifier * damage);
        }

        Debug.Log("Collide");
        RemoveBullet();
    }

    private void RemoveBullet()
    {
        Destroy(this.gameObject);
    }
}
