using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
  
  [SerializeField] private Button newGameButton;
  [SerializeField] private Button continueButton;
  [SerializeField] private Button quitButton;
  [SerializeField] private GameObject levelMenu;
  
  private void Awake()
  {
    levelMenu.SetActive(false);
    newGameButton.onClick.AddListener(() =>
    {
      PlayerPrefs.DeleteKey(PlayerPrefsKey.NumLevelUnlock);
      SceneLoader.Load(Scene.Level1);
    });
    continueButton.onClick.AddListener(() => levelMenu.SetActive(!levelMenu.activeSelf));
    quitButton.onClick.AddListener(Application.Quit);
  }
}
