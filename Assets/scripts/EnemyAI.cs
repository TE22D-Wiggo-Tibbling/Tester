using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

public Transform Player;

public float MinDist = 10;
public float MoveSpeed = 4;
public float MaxDist = 100;

private void Awake() {
    Player = GameObject.Find("Player").transform;
  
}

private void Update() {

 transform.LookAt(Player);
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

      
    
}

private void OnCollisionEnter(Collision other) {
    if(other.gameObject.tag == "Player"){
     if (other.gameObject.GetComponent<Health>() != null){
            other.gameObject.GetComponent<Health>().health -= 25;
         }
    Destroy(this.gameObject);
    }
}


}
