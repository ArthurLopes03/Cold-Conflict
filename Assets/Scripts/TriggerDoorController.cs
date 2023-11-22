using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.SetTrigger("OpenDoor");
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                myDoor.SetTrigger("CloseDoor");
                gameObject.SetActive(false);
            }
        }
    }
}
