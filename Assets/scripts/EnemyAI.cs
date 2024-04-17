using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
public NavMeshAgent agent;

public Transform player;

public LayerMask ground, whatIsPayer;

// Patroling
public Vector3 walkPoint;
bool walkPointSet;
public float walkPointRange;

// Attacking
public float timeBetwenAttack;
bool alredyAttacked;

// States
public float sightRange, attackRange;
public bool playerInSightRange, playerInAttackRange;

private void Awake() {
    player = GameObject.Find("Player").transform;
    agent = GetComponent<NavMeshAgent>();
}

private void Update() {
    // check for sight and attack range
    playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPayer);
    playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPayer);

    if(!playerInSightRange && !playerInAttackRange) Patroling();
    if(!playerInSightRange && playerInAttackRange) Chasing();
    if(playerInSightRange && playerInAttackRange) Attacking();

    Rigidbody rb = GetComponent<Rigidbody>();
    
}

private void Patroling(){
    if(!walkPointSet) SerchWalkPoint();  

    if(walkPointSet){
        agent.SetDestination(walkPoint);
    }

    Vector3 distanceToWalkPoint = transform.position - walkPoint;

    // Walkpoint Reched
    if(distanceToWalkPoint.magnitude < 1f){
        walkPointSet=false;
    }
}
private void SerchWalkPoint(){
    // Calculate random point in range
    float randomZ = Random.Range(-walkPointRange,walkPointRange);
    float randomX = Random.Range(-walkPointRange,walkPointRange);

    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    if(Physics.Raycast(walkPoint, -transform.up,ground)){
        walkPointSet = true;
    }
}
private void Chasing(){
agent.SetDestination(player.position);
}
private void Attacking(){
// Stoping enemy
agent.SetDestination(transform.position);
transform.LookAt(player);

if(!alredyAttacked){
    // Attack

    // 
    alredyAttacked = true;
    Invoke(nameof(ResetAttack), timeBetwenAttack);
}
}

private void ResetAttack()
{
alredyAttacked = false;
}
}
