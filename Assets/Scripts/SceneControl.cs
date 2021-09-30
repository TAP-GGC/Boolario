using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void SwitchToGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void SwitchToStats() {
        SceneManager.LoadScene("Statistics");
    }

    public void SwitchToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToInstructions() {
        SceneManager.LoadScene("Instructions");
    }
}
