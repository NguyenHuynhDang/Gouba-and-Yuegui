using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TutorialUI : MonoBehaviour
{
  public static event EventHandler OnHide;
  
  [SerializeField] private Tutorial tutorial;
  [SerializeField] private int tutorialIndex;
 
  private GameObject currentTutorial;
  
  private void Start()
  {
    Show();
    if (tutorialIndex < tutorial.Size())
      Instantiate(tutorialIndex);
    else
      Hide();

    GameInput.Instance.OnHelp += (_, _) =>
    {
      tutorialIndex = 0;
      Show();
      if (tutorialIndex < tutorial.Size())
        Instantiate(tutorialIndex);
      else
        Hide();
    };

    InputSystem.onAnyButtonPress.Call(_ =>
    {
      if (!this) return;
      Destroy(currentTutorial);

      if (tutorialIndex + 1 < tutorial.Size())
      {
        tutorialIndex++;
        Instantiate(tutorialIndex);
      }
      else
      {
        Hide();
      }
    });
  }
  
  private void Instantiate(int index)
  {
    currentTutorial = Instantiate(tutorial.GetTutorial(index), Vector3.zero, Quaternion.identity);
    currentTutorial.transform.SetParent(transform, false);
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    OnHide?.Invoke(this, EventArgs.Empty);
    gameObject.SetActive(false);
  }
}
