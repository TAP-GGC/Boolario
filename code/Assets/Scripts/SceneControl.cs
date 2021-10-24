using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static bool andGameIndicator;

    public void SwitchToHowToScore() {
        SceneManager.LoadScene("HowToScore");
    }

    public void SwitchToGame() {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

    public void EnableAndGame() {
        andGameIndicator = true;
    }

    public void EnableOrGame() {
        andGameIndicator = false;
    }

    public void SwitchToGameSelection() {
        SceneManager.LoadScene("GameSelection");
    }

    public void SwitchToStats() {
        SceneManager.LoadScene("Statistics");
    }

    public void SwitchToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToInstructionMenu() {
        SceneManager.LoadScene("InstructionsMenu");
    }

    public void SwitchToHowToPlay() {
        SceneManager.LoadScene("HowToPlay");
    }

    public void SwitchToControls() {
        SceneManager.LoadScene("Controls");
    }
}
