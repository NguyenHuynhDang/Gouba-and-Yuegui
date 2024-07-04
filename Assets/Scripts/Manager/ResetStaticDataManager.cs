using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
  private void Awake()
  {
    Reset();
  }

  public void Reset()
  {
    Goal.ResetStaticData();
    GameClearUI.ResetStaticData();
    CharacterManager.ResetStaticData();
    RestartUI.ResetStaticData();
    BackMenuUI.ResetStaticData();
  }
}
