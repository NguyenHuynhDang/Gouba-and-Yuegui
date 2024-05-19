using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
  public static event EventHandler OnGameClear;
  
  public int NumPlayerReach { get; private set; }
  

  public void OnEnter()
  {
    NumPlayerReach++;
    
    if (NumPlayerReach == PlayerManager.NumPlayer)
    {
      OnGameClear?.Invoke(this, EventArgs.Empty);
    }
  }

  public void OnExit()
  {
    NumPlayerReach--;
  }

  public static void ResetStaticData()
  {
    OnGameClear = null;
  }
}
