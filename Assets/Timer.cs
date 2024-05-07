using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float timer = 100;
    public Text timerText;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text =timer.ToString();
    }
}
