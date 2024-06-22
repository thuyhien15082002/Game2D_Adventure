using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 400;
    public GameObject gameOverMenu;
    public TextMeshProUGUI scoreOver;
    public GameObject winMenu;
    public TextMeshProUGUI scoreWin;
     private SpriteRenderer sprite;

    
    

    //check active state
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isDeath = false;

    public Transform attackPoint;
    public LayerMask enemyLayer;
    public int attackDamage = 50;

    // music
    private AudioManager audioManager;

    private float horizontal;
    private string currentAnim;

    // coin 
    public TextMeshProUGUI txtCoin;

    private int numCoin = 0;

    private int deathCount = 0;
    private const int maxDeaths=3;

    private Vector3 savePoint;
    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
        winMenu.SetActive(false);
        SavePoint();
        OnInit();
    }

    void FixedUpdate()
    {
        if (isDeath)
        {
            return;
        }

        isGrounded = CheckGrounded();
        horizontal = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
                //anim.SetTrigger("run");
            }
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");

            }
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                ChangeAnim("slash");
                Slash();

            }


        }
        else if (!isGrounded && Input.GetKeyDown(KeyCode.V))
        {
            ChangeAnim("hurt");
            Slash();
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
          //  sprite.flipX = false;
           transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }


    private void OnInit()
    {
        if (deathCount >= maxDeaths)
        {
            ShowGameOverMenu();
            return;
        }
        isDeath = false;
       
        transform.position = savePoint;
        ChangeAnim("idle");
    }
    private bool CheckGrounded()
    {

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.black);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;

    }
    private void Slash()
    {
        //goi phuong thuc cu the sau 1f
        Invoke(nameof(ResetSlash), 1f);
        //xac dinh all collider, tim collider co tam attacPo
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }
    

    private void ResetSlash()
    {
        ChangeAnim("idle");

    }
    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);

    }

    private void ChangeAnim(string animName)
    {

        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);

        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }
    
    internal void SavePoint(){
        savePoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
            numCoin++;
            txtCoin.text = "" + numCoin;
            audioManager.PlaySFX(audioManager.coinClip);

        }
       
        if (other.tag == "Win")
        {
            if(numCoin >= 50){
            audioManager.PlaySFX(audioManager.winClip);
            WinGame();
            }else{
                ShowGameOverMenu();
            }

         }
        
        if (other.tag == "Death")
        {
            deathCount++;
            if (deathCount < maxDeaths)
            {
                isDeath = true;
                ChangeAnim("death");
                Invoke(nameof(OnInit), 0.5f); //chet trong 1s khoi tao oninit
                audioManager.PlaySFX(audioManager.deathClip);
            }else{
                ShowGameOverMenu();
            }

        }

    }
    private void ShowGameOverMenu(){
        PlayerPrefs.SetInt("CoinCollected", numCoin);
        PlayerPrefs.Save();
        gameOverMenu.SetActive(true);   
        Time.timeScale = 0f;
        scoreOver.text = "" + numCoin;

    }
    private void WinGame()
    {
        PlayerPrefs.SetInt("CoinCollected", numCoin);
        PlayerPrefs.Save();
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        scoreWin.text = "" + numCoin;

    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
            Gizmos.DrawWireSphere(attackPoint.position, 0.5f); //
        }

    }
}
