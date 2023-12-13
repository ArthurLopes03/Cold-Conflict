
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

    void Update()
    {
        UnityEngine.Debug.Log("Score is" + score);
        scoreUI.text = score.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Collider is Working");
        if (other.gameObject.tag == "enemy")
        {
            score++;
        }
    }

    void OnDestroy()
    {
        UnityEngine.Debug.Log("shit has been destroyed");
        score++;
    }

}


