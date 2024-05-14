using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    float timer = 100;
    public Text timerText;
   
    void Update()
    {
        timer -= Time.deltaTime;
        int sekunder = Mathf.FloorToInt(timer);

        timerText.text =sekunder.ToString();

        if(timer < 0){
            SceneManager.LoadScene(3);
        }
    }
}
