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

    // Update is called once per frame
    void Update()
    {
        score.text =  "Score:\n" + PlayerController.score;
    }
}
