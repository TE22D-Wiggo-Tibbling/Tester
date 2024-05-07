using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    void Update()
    {
        if(this.gameObject.GetComponent<Health>().health <=0){
            SceneManager.LoadScene(1);
        }
    }
}
