using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    
    
    public void ReplayGame() 
    {
        SceneManager.LoadScene("Level");
    }

    public void ReplayGameTwo()
    {
        SceneManager.LoadScene("Level 2");
        
    }

    public void nextLevelTwo() 
    {
        SceneManager.LoadScene("Level 2");
        

    }

    public void nextLevelThree() 
    {
        SceneManager.LoadScene("Level 3");
    }

    public void QuitGame() 
    {
        Application.Quit();

    }
}
