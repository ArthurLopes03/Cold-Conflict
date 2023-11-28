using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject[] weapons;
    public int playerHealth;
    public int currentEquipIndex;
    public Gun currentEpuipGun;
    public TextMeshProUGUI textMesh;

    public KeyCode mainWeapon = KeyCode.Alpha1;
    public KeyCode sideWeapon = KeyCode.Alpha2;

    [Header("Weapon UI")]
    public Image weaponPanel;
    public Sprite[] weaponSlots;

    private void Start()
    {
        currentEquipIndex = 0;
        weapons[1].SetActive(false);
        textMesh.text = playerHealth.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentEquipIndex != 0)
        {
            Debug.Log("Changing to Main Weapon");
            SwitchWeapons(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && currentEquipIndex != 1)
        {
            Debug.Log("Changing to Side Weapon");
            SwitchWeapons(1);
        }
    }

    public void SwitchWeapons(int index)
    {
        weaponPanel.sprite = weaponSlots[index];
        weapons[currentEquipIndex].SetActive(false);
        weapons[index].SetActive(true);
        weapons[index].GetComponent<Animator>().SetTrigger("Draw");
        currentEquipIndex = index;
        currentEpuipGun = weapons[currentEquipIndex].GetComponent<Gun>();
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        textMesh.text = playerHealth.ToString();
    }
}
