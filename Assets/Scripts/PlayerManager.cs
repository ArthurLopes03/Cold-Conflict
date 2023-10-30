using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] weapons;
    public int playerHealth;
    public int currentEquip;

    public KeyCode mainWeapon = KeyCode.Alpha1;
    public KeyCode sideWeapon = KeyCode.Alpha2;

    private void Start()
    {
        currentEquip = 0;
        weapons[1].SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentEquip != 0)
        {
            Debug.Log("Changing to Main Weapon");
            SwitchWeapons(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currentEquip != 1)
        {
            Debug.Log("Changing to Side Weapon");
            SwitchWeapons(1);
        }
    }

    public void SwitchWeapons(int index)
    {
        weapons[currentEquip].SetActive(false);
        weapons[index].SetActive(true);
        weapons[index].GetComponent<Animator>().SetTrigger("Draw");
        currentEquip = index;
    }
}