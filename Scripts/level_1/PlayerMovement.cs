using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jump = 7f;
    private bool ground;
    private BoxCollider2D coll;

    
   

    private enum MovementState{ idle, running, jumping}
    private MovementState state = MovementState.idle;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); 
        
    }


    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if(Input.GetButtonDown("Jump")){
            if(ground == true){
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
            ground = false;

        }
        UpdateAnimation(); 
    }
    
   private void UpdateAnimation(){
    MovementState state;
 
        if(dirX > 0f){
            state = MovementState.running;
            sprite.flipX = false;
        }else if(dirX < 0f){
            state = MovementState.running;
            sprite.flipX = true;
        }else{
            state = MovementState.idle;
        }
        if(rb.velocity.y > .1f){
            state = MovementState.jumping;
        }
        anim.SetInteger("state", (int)state);
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = true;
            
        }
        
    }
    
}
