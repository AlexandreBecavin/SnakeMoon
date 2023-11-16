using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    void Start()
    {
        scoreText.text = "Score: 0";
    }
    // Start is called before the first frame update
    public void increaseScore() {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void resetScore() {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
