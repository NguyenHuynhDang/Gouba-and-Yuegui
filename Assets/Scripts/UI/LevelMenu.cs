using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
  private Button[] buttons;

  private void Awake()
  {
    SetButtonArray();
    
    int numLevelUnlock = PlayerPrefs.GetInt(PlayerPrefsKey.NumLevelUnlock, 1);
    
    for (int i = 0; i < numLevelUnlock; i++)
    {
      buttons[i].interactable = true;
      int levelIndex = i + 2;
      buttons[i].onClick.AddListener(() => SceneLoader.Load((Scene)levelIndex));
    }

    for (int i = numLevelUnlock; i < buttons.Length; i++)
    {
      buttons[i].interactable = false;
    }
  }
  
  private void SetButtonArray()
  {
    int childCount = transform.childCount;
    buttons = new Button[childCount];
    for (int i = 0; i < childCount; i++)
    {
      buttons[i] = transform.GetChild(i).GetComponent<Button>();
    }
  }
}
