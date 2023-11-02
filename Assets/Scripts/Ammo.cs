using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float existanceTime = 10f;
    public int damage;

    private void Start()
    {
        Invoke(nameof(RemoveBullet), existanceTime);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "HitBox")
        {
            collision.gameObject.GetComponentInParent<EnemyManager>().health -= collision.gameObject.GetComponent<HitBox>().hitModifier * damage;
            Debug.Log("Hit for " + collision.gameObject.GetComponent<HitBox>().hitModifier * damage);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }

        RemoveBullet();
    }

    private void RemoveBullet()
    {
        Destroy(this.gameObject);
    }
}
