using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public void Shoot()
    {
        Debug.Log("SHoot");
        Transform gunBarrel = enemy.gunBarrel;
        enemy.gunSound.Play();
        Vector3 dir = (enemy.Player.transform.position - gunBarrel.transform.position).normalized + new Vector3(Random.Range(-enemy.maxSpread, enemy.maxSpread), Random.Range(-enemy.maxSpread, enemy.maxSpread), Random.Range(-enemy.maxSpread, enemy.maxSpread));
        GameObject bullet = GameObject.Instantiate(enemy.ammoType, gunBarrel.position, gunBarrel.transform.rotation * Quaternion.AngleAxis(90, Vector3.right));
        bullet.GetComponent<Rigidbody>().AddForce(dir * enemy.bulletSpeed, ForceMode.Impulse);
        shotTimer = 0;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            Vector3 targetPostition = new Vector3(enemy.Player.transform.position.x, enemy.transform.position.y, enemy.Player.transform.position.z);
            enemy.transform.LookAt(targetPostition);



            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            enemy.LastKnownPOS = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new SearchState());   
            }   
        }
    }
}
