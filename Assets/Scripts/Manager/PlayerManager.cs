using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public static int NumPlayer { get; private set; }
  
  [SerializeField] private List<Player> playerList;
  [SerializeField] private List<CharacterButtonUI> characterUI;
  [SerializeField] private Transform arrow;
  
  private Player currentPlayer;
  private CharacterButtonUI currentUI;
  private Vector3 direction;
  
  private void Start()
  {
    NumPlayer = playerList.Count;
    
    currentPlayer = playerList[0];
    SetArrowParent();
    
    currentUI = characterUI[0];

    for (int i = 0; i < playerList.Count; i++)
    {
      characterUI[i].SetRemainStepText(playerList[i].GetRemainMove());
    }

    //GameInput.Instance.OnMove += GameInput_OnMove;
    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch1;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch2;
  }

  private void Update()
  {
    direction = GameInput.Instance.GetMovementVector();
    if (direction != Vector3.zero)
    {
      currentPlayer.MovePlayer(direction);
      currentUI.SetRemainStepText(currentPlayer.GetRemainMove());
    }
  }

  private void GameInput_OnSwitch1(object sender, OnSwitchCharacterEventArgs e)
  {
    currentPlayer = playerList[0];
    currentUI = characterUI[0];
    arrow.SetParent(currentPlayer.transform, false); 
  }

  private void GameInput_OnSwitch2(object sender, OnSwitchCharacterEventArgs e)
  {
    if (playerList.Count > 1)
    {
      currentPlayer = playerList[1];
      currentUI = characterUI[1];
      SetArrowParent();
    }
  }
  
  private void SetArrowParent()
  {
    arrow.SetParent(currentPlayer.transform, false);
  }
}
