using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinCollection : MonoBehaviour
{
    private int coins = 0;
    private AudioManager audioManager;
    
    
   public TextMeshProUGUI coinsText;
      private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("coin")){
            Destroy(collision.gameObject);
            audioManager.PlaySFX(audioManager.coinClip);
            coins++;
            coinsText.text = "" +coins;  
        }
         if(collision.gameObject.CompareTag("Zombie")){
          collision.GetComponent<Zombie>().TakeDamage(25);
       //  Destroy(collision.gameObject);
         }
    
    }
    
 
}
