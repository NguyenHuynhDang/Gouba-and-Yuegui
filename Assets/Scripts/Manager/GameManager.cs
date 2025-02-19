using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static bool IsGamePause { get; private set; }
  public static bool IsGamePlaying { get; private set; }
  
  private void Start()
  {
    IsGamePlaying = false;
    
    GameInput.Instance.OnEscape += (_, _) =>
    {
      IsGamePause = !IsGamePause;
      Time.timeScale = 1 - Time.timeScale;
    };

    GameInput.Instance.OnHelp += (_, _) =>
    {
      IsGamePlaying = false;
    };
    
    TutorialUI.OnHide += TutorialUI_OnHide;
    Goal.OnGameClear += OnGameClear;
    RestartUI.OnRestart += OnRestart;
    GameClearUI.OnNextLevel += OnNextLevel;
    GameClearUI.OnMainMenu += OnMainMenu;
    BackMenuUI.OnMainMenu += OnMainMenu;
  }

  private void TutorialUI_OnHide(object sender, EventArgs e)
  {
    IsGamePlaying = true;
  }

  private void OnMainMenu(object sender, EventArgs e)
  {
    IsGamePause = false;
    Time.timeScale = 1f;
    SceneManager.LoadScene(Scene.MainMenu.ToString());
  }

  private void OnGameClear(object sender, EventArgs e)
  {
    Time.timeScale = 0f;
    if (IsLastLevelUnlock() && HasNextLevel(out _))
    {
      PlayerPrefs.SetInt(PlayerPrefsKey.NumLevelUnlock,
                        PlayerPrefs.GetInt(PlayerPrefsKey.NumLevelUnlock, 1) + 1);
      PlayerPrefs.Save();
    }
  }

  private void OnNextLevel(object sender, OnNextLevelEventArgs e)
  {
    Time.timeScale = 1f;
    SceneLoader.Load(e.NextLevel);
  }

  private void OnRestart(object sender, EventArgs e)
  {
    IsGamePause = false;
    Time.timeScale = 1f;
    SceneLoader.Load((Scene)SceneManager.GetActiveScene().buildIndex);
  }

  private bool IsLastLevelUnlock() // is this level the last level to be unlocked
  {
    int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
    return currentLevel == PlayerPrefs.GetInt(PlayerPrefsKey.NumLevelUnlock, 1);
  }
  
  public static bool HasNextLevel(out int nextLevel)
  {
    int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (SceneManager.sceneCountInBuildSettings > nextLevelIndex)
    {
      nextLevel = nextLevelIndex;
      return true;
    }

    nextLevel = -1;
    return false;
  }
}
