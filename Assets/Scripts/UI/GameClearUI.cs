using System;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
  public static event EventHandler<OnNextLevelEventArgs> OnNextLevel;
  public static event EventHandler OnMainMenu;
  
  [SerializeField] private Button mainMenuButton;
  [SerializeField] private Button nextLevelButton;
  [SerializeField] private GameObject statusText;
  
  private void Awake()
  {
    mainMenuButton.onClick.AddListener(() => OnMainMenu?.Invoke(this, EventArgs.Empty));
    nextLevelButton.onClick.AddListener(() =>
    {
      if (GameManager.HasNextLevel(out int nextLevel)) // has next level
      {
        OnNextLevel?.Invoke(this, new OnNextLevelEventArgs { NextLevel = (Scene)nextLevel});
      }
      else
      {
        statusText.SetActive(true);
      }
    });
  }

  private void Start()
  {
    Goal.OnGameClear += (_, _) => { Show(); };
    statusText.SetActive(false);
    Hide();
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  public static void ResetStaticData()
  {
    OnMainMenu = null;
    OnNextLevel = null;
  }
}
