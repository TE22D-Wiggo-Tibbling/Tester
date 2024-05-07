using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        if (this.gameObject.tag == "Player") health = 100;
        if (this.gameObject.tag == "Enemy") health = 25;



    }

    // Update is called once per frame
    void Update()
    {
         if (this.gameObject.tag == "Enemy"){

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
         }
    }

}
