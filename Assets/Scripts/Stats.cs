using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text highscoreTrack, deathTrack, trueTrack, falseTrack, totalCoinTrack;
    
    void Start()
    {
        highscoreTrack = GameObject.Find("HighScoreTracker").GetComponent<TMP_Text>();
        deathTrack = GameObject.Find("DeathCounter").GetComponent<TMP_Text>();
        trueTrack = GameObject.Find("TrueCoinsCollected").GetComponent<TMP_Text>();
        falseTrack = GameObject.Find("FalseCoinsCollected").GetComponent<TMP_Text>();
        totalCoinTrack = GameObject.Find("TotalCoinsCollected").GetComponent<TMP_Text>();
        //scoreUponDeath = GameObject.Find("ScoreUponDeath").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update() {
    
        highscoreTrack.text =  "" + PlayerController.highScore;
        deathTrack.text =  "Total Deaths: " + PlayerController.deathCounter;
        trueTrack.text =  "Total True Coins Collected: " + PlayerController.trueCoinsCollected;
        falseTrack.text =  "Total False Coins Collected: " + PlayerController.falseCoinsCollected;
        totalCoinTrack.text = "Total Coins Collected: " + PlayerController.totalCoinsCollected;
        //scoreUponDeath.text = "Score\n" + PlayerController.score;
    }
}
