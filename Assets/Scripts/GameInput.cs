using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
  public static GameInput Instance { get; private set; }
  public event EventHandler<OnSwitchCharacterEventArgs> OnSwitch1;
  public event EventHandler<OnSwitchCharacterEventArgs> OnSwitch2;
  public event EventHandler OnEscape;
  public event EventHandler OnRestart;

  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    Instance = this;

    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();
  }

  private void OnDestroy()
  {
    playerInputActions.Dispose();
  }

private void Start()
  {
    playerInputActions.Player.Switch1.performed += _ =>
    {
      if (GameManager.IsGamePause) return;
      OnSwitch1?.Invoke(this, new OnSwitchCharacterEventArgs {Character = CharacterType.Gouba});
    };
    
    playerInputActions.Player.Switch2.performed += _ =>
    {
      if (GameManager.IsGamePause) return;
      OnSwitch2?.Invoke(this, new OnSwitchCharacterEventArgs {Character = CharacterType.Yuegui});
    };

    playerInputActions.Player.Replay.performed += _ =>
    {
      if (GameManager.IsGamePause) return;
      OnEscape?.Invoke(this, EventArgs.Empty);
      OnRestart?.Invoke(this, EventArgs.Empty);
    };
    
    playerInputActions.Player.Escape.performed += _ => { OnEscape?.Invoke(this, EventArgs.Empty); };
  }

  public Vector2 GetMovementVector()
  {
    Vector2 movementVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    return Normalize(movementVector);
  }

  private Vector2 Normalize(Vector2 movementVector)
  {
    if (movementVector.x != 0) // is moving on the x axis
    {
      movementVector.y = 0;
      movementVector.Normalize();
    }
    else if (movementVector.y != 0) // is moving on the y axis
    {
      movementVector.x = 0;
      movementVector.Normalize();
    }
    return movementVector;
  }
}
