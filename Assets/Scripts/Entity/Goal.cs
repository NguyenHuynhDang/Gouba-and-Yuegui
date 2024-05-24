using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
  public static event EventHandler OnGameClear;
  
  public int NumCharacterReach { get; private set; }
  

  public void OnEnter()
  {
    NumCharacterReach++;
    
    if (NumCharacterReach == CharacterManager.NumCharacter)
    {
      OnGameClear?.Invoke(this, EventArgs.Empty);
    }
  }

  public void OnExit()
  {
    NumCharacterReach--;
  }

  public static void ResetStaticData()
  {
    OnGameClear = null;
  }
}
