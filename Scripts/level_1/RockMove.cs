using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{

    public float left;
    public float right;
    public bool isRight;
    void Start()
    {
      
    }


    // Update is called once per frame
    void Update()
    {
        var rock = transform.position.x;
        if(rock < left){
            isRight = true;
        }
        if(rock > right){
            isRight = false;
        }
        if(isRight){
            transform.Translate(new Vector3(Time.deltaTime * 1, 0, 0)) ;
        }else{
            transform.Translate(new Vector3(-Time.deltaTime * 1, 0, 0)) ;
        }
    }
}
