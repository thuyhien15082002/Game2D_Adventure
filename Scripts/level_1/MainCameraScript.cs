using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject player;
    public float start, end;
    void Start()
    {

    }
    void Update()
    {
        
        var player_x = player.transform.position.x;
        
        var camera_x = transform.position.x;
        var camera_y = transform.position.y;
        var camera_z = transform.position.z;

        if(player_x > start && player_x <end){
            camera_x = player_x;
        }else{
            if(player_x < start){
                camera_x = start;
            }
            if(player_x > end){
                camera_x = end;
            }
        }
      
        transform.position = new Vector3(camera_x,camera_y, camera_z);
    }
}
