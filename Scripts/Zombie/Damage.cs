using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Head")){
          collision.GetComponent<Zombie>().TakeDamage(25);
            
        }
       }
}
