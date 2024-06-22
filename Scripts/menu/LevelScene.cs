using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public void BackMainScene(){
        SceneManager.LoadScene("StartScene");
         Time.timeScale = 1f;
    }

    public void LoadScene(string levelName){
        SceneManager.LoadScene(levelName);
         Time.timeScale = 1f;
    }
}
