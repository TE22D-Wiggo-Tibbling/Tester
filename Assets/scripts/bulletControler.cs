using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControler : MonoBehaviour
{
   public Rigidbody bulletRigidbody;

   public float speed = 10f;

   public float damage = 25;

   private void Awake()
   {
      // bulletRigidbody = GetComponent<Rigidbody>();
   }

   private void Start()
   {
      bulletRigidbody.velocity = transform.forward * speed;
   }

   private void OnTriggerEnter(Collider collider)
   {
      if (collider.gameObject.tag != "Player")
      {
         Destroy(gameObject);

         if (collider.GetComponent<Health>() != null){

            collider.gameObject.GetComponent<Health>().health -= 25;
         }
      }
   }
}
