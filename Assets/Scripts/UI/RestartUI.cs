using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartUI : MonoBehaviour
{
  public static event EventHandler OnRestart;
  
  [SerializeField] private Button noButton;
  [SerializeField] private Button yesButton;

  private bool isActive;
  
  private void Awake()
  {
    noButton.onClick.AddListener(Hide);
    yesButton.onClick.AddListener(() => OnRestart?.Invoke(this, EventArgs.Empty));
  }

  private void Start()
  {
    GameInput.Instance.OnEscape += GameInput_OnEscape;
    GameInput.Instance.OnRestart += (_, _) => { Show(); };
    Hide();
  }

  private void GameInput_OnEscape(object sender, EventArgs e)
  {
    if (!isActive) return;
    Hide();
  }

  private void Show()
  {
    isActive = true;
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    isActive = false;
    gameObject.SetActive(false);
  }

  public static void ResetStaticData()
  {
    OnRestart = null;
  }
}
