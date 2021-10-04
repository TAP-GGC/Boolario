using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public bool andGameIndicator;
    public void SwitchToAndGame() {
        SceneManager.LoadScene("SampleScene");
        andGameIndicator = true;
        Time.timeScale = 1f;
    }

    public void SwitchToOrGame()
    {
        SceneManager.LoadScene("SampleScene");
        andGameIndicator = false;
        Time.timeScale = 1f;
    }

    public void SwitchToStats() {
        SceneManager.LoadScene("Statistics");
        Time.timeScale = 0;
    }

    public void SwitchToMainMenu() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 0;
    }

    public void SwitchToInstructions() {
        SceneManager.LoadScene("Instructions");
        Time.timeScale = 0;
    }
}
