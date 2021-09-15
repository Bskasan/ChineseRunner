using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecond;
    public bool scoreIncreasing;

    
    // Start is called before the first frame update
    void Start()
    {
        hiScoreCount = scoreCount;
        hiScoreCount = PlayerPrefs.GetFloat("Highscore");
        

    }

    


    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
            scoreCount += pointsPerSecond * Time.deltaTime; // increasing per frame

        //PlayerPrefs.SetFloat("scoreCount", scoreCount);

        if (scoreCount > hiScoreCount) 
        {
            hiScoreCount = scoreCount;
            PlayerPrefs.SetFloat("Highscore", scoreCount);
        }

        


        scoreText.text = "Score : " + Mathf.Round(scoreCount);
        hiScoreText.text = "High Score : " + Mathf.Round(hiScoreCount);

    }
}
