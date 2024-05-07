using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float timer = 0;
    public float timeNeded = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    int random = Random.Range(-100,100);

     Vector3 spawnPosition = new(random,2,random);
     Quaternion quaternion = new(1,1,1,1);

        timer+=Time.deltaTime;
        if(timer>timeNeded){
        Instantiate(Enemy,spawnPosition,quaternion);
        timer=0;
        }
    }
}