using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class TutorialUI : MonoBehaviour
{
  [SerializeField] private List<GameObject> tutorials;
  private int currentIndex;
  private GameObject currentTutorial;
  
  private void Start()
  {
    Show();
    currentIndex = 0;
    Instantiate(currentIndex);
    
    InputSystem.onAnyButtonPress.Call(_ =>
    {
      if (!this)
      {
        return;
      }
      Destroy(currentTutorial);

      if (currentIndex + 1 < tutorials.Count)
      {
        currentIndex++;
        Instantiate(currentIndex);
      }
      else
      {
        Hide();
      }
    });
  }
  
  private void Instantiate(int index)
  {
    currentTutorial = Instantiate(tutorials[index], Vector3.zero, Quaternion.identity);
    currentTutorial.transform.SetParent(transform, false);
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }
}
