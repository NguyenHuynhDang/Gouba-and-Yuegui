using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
  public static event EventHandler OnStopMoving;
  public static int NumCharacter { get; private set; }
  
  [SerializeField] private List<Character> characterList;
  [SerializeField] private List<CharacterButtonUI> characterUI;
  [SerializeField] private Transform arrow;
  
  private Character currentCharater;
  private CharacterButtonUI currentUI;

  private void Start()
  {
    NumCharacter = characterList.Count;
    
    currentCharater = characterList[0];
    SetArrowParent();
    
    currentUI = characterUI[0];

    for (int i = 0; i < characterList.Count; i++)
    {
      characterUI[i].SetRemainStepText(characterList[i].GetRemainMove());
    }

    //GameInput.Instance.OnMove += GameInput_OnMove;
    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch1;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch2;
  }

  private void Update()
  {
    Vector2 movementVector = GameInput.Instance.GetMovementVector();
    if (movementVector != Vector2.zero)
    {
      if (!GameManager.IsGamePlaying) return;
      currentCharater.MoveCharacter(movementVector); 
      currentUI.SetRemainStepText(currentCharater.GetRemainMove());
    }
    else
    {
      if (!GameManager.IsGamePlaying) return;
      OnStopMoving?.Invoke(this, EventArgs.Empty);
    }
  }

  private void GameInput_OnMove(object sender, OnMoveEventArgs e)
  {
    currentCharater.MoveCharacter(e.MovementVector); 
    currentUI.SetRemainStepText(currentCharater.GetRemainMove());
  }

  private void GameInput_OnSwitch1(object sender, OnSwitchCharacterEventArgs e)
  {
    if (currentCharater != characterList[0])
    {
      currentCharater = characterList[0];
      currentUI = characterUI[0];
      SetArrowParent();
    }
  }

  private void GameInput_OnSwitch2(object sender, OnSwitchCharacterEventArgs e)
  {
    if (characterList.Count > 1 && currentCharater != characterList[1])
    {
      currentCharater = characterList[1];
      currentUI = characterUI[1];
      SetArrowParent();
    }
  }
  
  private void SetArrowParent()
  {
    arrow.SetParent(currentCharater.transform, false);
  }

  public static void ResetStaticData()
  {
    OnStopMoving = null;
  }
}
