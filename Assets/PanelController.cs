using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    GameObject ScorePanel;
    GameObject MenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        ScorePanel = GameObject.Find("ScorePanel");
        MenuPanel = GameObject.Find("MenuPanel");
        ScorePanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void ActivateScorePanel() {
        ScorePanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
}
