using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OpenLevelSelect(){
        SceneManager.LoadScene("LevelScene");
    }
    public void SelectLevel2(){
        SceneManager.LoadScene("SceneLevel_2");
        Time.timeScale = 1f;
    }
    public void backMenuHome(){
        
        SceneManager.LoadScene("StartScene");
    }
    public void exitGame(){
        
        Application.Quit();
    }
}
