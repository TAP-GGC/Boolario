using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUponDeath : MonoBehaviour
{ 
    TMP_Text score;
    
    void Start()
    {
        score = GameObject.Find("ScoreUponDeath").GetComponent<TMP_Text>();
    }

    void Update()
    {
        score.text =  "" + PlayerController.score;
    }
}
