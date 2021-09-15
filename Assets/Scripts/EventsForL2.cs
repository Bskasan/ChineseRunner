using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsForL2 : MonoBehaviour
{
    

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ReplayGameTwo() 
    {
        SceneManager.LoadScene("Level 2");
    }

    


    public void QuitGame()
    {
        Application.Quit();

    }
}
