using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Zombie : MonoBehaviour
{
    Transform target;
    public Transform borderCheck;
    public int zombieHP = 100;
    public Animator animator;
    public Slider zombieHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        zombieHealthBar.value = zombieHP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
      //  Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.x > transform.position.x){
            transform.localScale = new Vector2(0.5f, 0.5f);
        }else{
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }
    }

    public void TakeDamage(int damageAmount){
        zombieHP -= damageAmount;
       zombieHealthBar.value = zombieHP;
        if(zombieHP > 0 )
        {
           animator.SetTrigger("damage");
        } else {
           animator.SetTrigger("death");
        //    GetComponent<CapsuleCollider2D>().enabled = false;
          //  this.enabled = false;
        }
    }
        
}