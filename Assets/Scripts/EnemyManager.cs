using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    // Update is called once per frame
    void Update()
    {
        if(health < 0)
            Destroy(this.gameObject);
    }
}
