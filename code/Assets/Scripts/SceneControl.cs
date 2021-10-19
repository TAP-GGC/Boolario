using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneControl : MonoBehaviour
{
    public static bool andGameIndicator;
    public void SwitchToLogicDescription() {
        SceneManager.LoadScene("LogicDescription");
        Time.timeScale = 0;
    }
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

    public void SwitchToInstructionMenu() {
        SceneManager.LoadScene("InstructionsMenu");
        Time.timeScale = 0;
    }

    public void SwitchToGameplayInstructions() {
        SceneManager.LoadScene("GameplayInstructions");
        Time.timeScale = 0;
    }

    public void SwitchToKeybindingsInstructions() {
        SceneManager.LoadScene("Keybindings");
        Time.timeScale = 0;
    }
}
