using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    void Start() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void SwitchToStats() {
        SceneManager.LoadScene("Statistics");
    }

    public void SwitchToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
