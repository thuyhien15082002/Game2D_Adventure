using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDead = false;
    public GameObject gameOverMenu;
    private Animator animation;
    public float fallThreshold = -3.973f;
    public TextMeshProUGUI  scoreOver;
    public  int numCoin = 0;
    public TextMeshProUGUI textCoins;
     private AudioManager audioManager;
     private int deathCount = 0;
    private const int maxDeaths=3;
    public GameObject winMenu;
    public TextMeshProUGUI scoreWin;
    private Vector3 savePoint;

     private void Start(){
        rb = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        winMenu.SetActive(false);
        gameOverMenu.SetActive(false);
     }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }

     private void Update(){
        numCoin = int.Parse(textCoins.text);  
        if (transform.position.y < fallThreshold && !isDead)
         {
            Die();
         }
     }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("saw") || collision.gameObject.CompareTag("spike")&& !isDead )
        {         
            Die();     
        }
        
        if(collision.gameObject.CompareTag("win") && !isDead){
            if(numCoin >= 30)
            {
             WinGame();
            } else {
                Die();
            }
        }
    }
        
 public void Die()
{
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        animation.SetTrigger("death");
        ShowGameOverMenu();
       
}
    private void ShowGameOverMenu(){
         audioManager.PlaySFX(audioManager.deathClip);
        PlayerPrefs.SetInt("CoinCollected", numCoin);
        PlayerPrefs.Save();
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        scoreOver.text = "" + numCoin;
    }

       private void WinGame()
    {
        audioManager.PlaySFX(audioManager.winClip);
        PlayerPrefs.SetInt("CoinCollected",numCoin);
        PlayerPrefs.Save(); 
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        scoreWin.text = "" + numCoin;
    }

       public void RestartGame()
     {
         if (isDead){

            gameOverMenu.SetActive(true);
            scoreOver.text = "" + numCoin;
      }     
     }

}



    

