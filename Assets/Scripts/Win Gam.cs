using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinGam : MonoBehaviour
{
    void OnTriggerEnter(Collider ChangeScene)
    {
        Debug.Log("raaahhhahah");
        if (ChangeScene.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("main menu");
        }

    }
}
