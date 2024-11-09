/*******************************************************************
* COPYRIGHT       : 2024
* PROJECT         : GDC: Jigsaw puzzle
* FILE NAME       : UIBehaviors.cs
* DESCRIPTION     : Level traversal and fuctions for UI
*                    
* REVISION HISTORY:
* Date 			Author    		        Comments
* ---------------------------------------------------------------------------
* 2024/11/03  Jayden Younger            Pause, reload, 3 levels traverse, and quit
* 2024/11/04  Jayden Younger            Add functions for each level
/******************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIBehaviors : MonoBehaviour
{
    // Reference to a pause menu UI element (if you have one)
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    // Call this method to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
        #else
        Application.Quit(); // Quit the application
        #endif
    }

    // Call this method to traverse to a specific level
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName); // Load the specified level
    }

    // Call this method to toggle pause and resume
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Freeze the game time
        isPaused = true; // Update the pause state
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true); // Show the pause menu
        }
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game time
        isPaused = false; // Update the pause state
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Hide the pause menu
        }
    }

    // Optional: You can add a method to reload the current level
    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void LevelSelect(){
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevel1(){
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadLevel2(){
        SceneManager.LoadSceneAsync(3);
    }

    public void LoadLevel3(){
        SceneManager.LoadSceneAsync(4);
    }

    public void LoadBonusLevel(){
        SceneManager.LoadSceneAsync(5);
    }
}

