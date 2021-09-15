using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    public static bool gameOver = false; // static means you can accept from other scripts
    public GameObject gameOverPanel;
    public GameObject levelUpPanel;
    public static bool nextLevel = false;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public Text coinsText;

    private ScoreManager theScoreManager;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1; // to prevent the player from stopping when we click on the replay button.
        isGameStarted = false;
        numberOfCoins = 0;
        nextLevel = false;

        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) 
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            theScoreManager.scoreCount = 0f;
        
        }

        if (nextLevel) 
        {
            Time.timeScale = 0;
            levelUpPanel.SetActive(true);
            PlayerPrefs.SetFloat("Score", theScoreManager.scoreCount);
        }


        coinsText.text = "Coins : " + numberOfCoins;

        if (SwipeManager.tap) 
        {
            isGameStarted = true;
            Destroy(startingText);
            theScoreManager.scoreIncreasing = true;
            
           
        }
    }
}
