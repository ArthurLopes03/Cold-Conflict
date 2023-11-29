using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public Image weaponIcon;
    public Sprite[] weaponSlots;
    private float alphaLevel = 1f;
    private float fadeTimer = 2.5f;

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

        if(playerHealth <= 0) 
        {
            EndScreen();
        }
    }

    private void FixedUpdate()
    {
        if (fadeTimer <= 0)
        {
            FadeIcon();
        }
        else
        { 
            fadeTimer -= 0.05f;
        }
    }

    public void SwitchWeapons(int index)
    {
        fadeTimer = 2.5f;
        alphaLevel = 1f;
        weaponIcon.color = new Color(1, 1, 1, alphaLevel);
        weaponIcon.sprite = weaponSlots[index];
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

    public void FadeIcon()
    {
        weaponIcon.color = new Color(1, 1, 1, alphaLevel);
        alphaLevel -= 0.05f;
    }

    public void EndScreen()
    {
        SceneManager.LoadScene("Death Screen");
    }
}
