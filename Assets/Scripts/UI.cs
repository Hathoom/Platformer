using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{

    private float accumulatedTime = 0f;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI coinstext;

    public float startTime = 400;
    private int totalscore = 0;
    private int totalcoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        
        if (accumulatedTime > 1f)
        {
            startTime -= 1f;
            accumulatedTime = 0f;

            //Debug.Log($"Time is {totalTime}");
        }

        timer.text = "Time: " + startTime.ToString();
    }

    public void UpdateScore(int score)
    {
        totalscore = totalscore + score;

        scoretext.text = "" + totalscore.ToString();
    }

    public void UpdateCoins()
    {
        totalcoins++;

        coinstext.text = "Coins: " + totalcoins.ToString();
    }
}
