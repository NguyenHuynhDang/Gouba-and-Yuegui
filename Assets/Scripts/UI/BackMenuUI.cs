using System;
using UnityEngine;
using UnityEngine.UI;

public class BackMenuUI : MonoBehaviour
{
  public static event EventHandler OnMainMenu;

  [SerializeField] private Button mainMenuButton;
  [SerializeField] private Button closeButton;

  private bool isActive;
  
  private void Awake()
  {
    mainMenuButton.onClick.AddListener(() => OnMainMenu?.Invoke(this, EventArgs.Empty));
    closeButton.onClick.AddListener(Hide);
  }

  private void Start()
  {
    GameInput.Instance.OnEscape += GameInput_OnEscape;
    Hide();
  }
  
  private void GameInput_OnEscape(object sender, EventArgs e)
  {
    if (isActive) Hide();
    else Show();
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
    OnMainMenu = null;
  }
}
