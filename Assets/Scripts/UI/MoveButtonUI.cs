using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveButtonUI : MonoBehaviour
{
  [SerializeField] private Button upButton;
  [SerializeField] private Button downButton;
  [SerializeField] private Button leftButton;
  [SerializeField] private Button rightButton;
    
  [SerializeField] private Color normalColor;
  [SerializeField] private Color pressedColor;

  private PlayerInputActions playerInputActions;

  private Image upButtonImage;
  private Image downButtonImage;
  private Image leftButtonImage;
  private Image rightButtonImage;

  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();
  }

  private void OnDestroy()
  {
    playerInputActions.Dispose();
  }

  private void Start()
  {
    upButtonImage = upButton.GetComponent<Image>();
    downButtonImage = downButton.GetComponent<Image>();
    leftButtonImage = leftButton.GetComponent<Image>();
    rightButtonImage = rightButton.GetComponent<Image>();
    
    playerInputActions.Player.Move.performed += Move_performed;
    playerInputActions.Player.Move.canceled += Move_canceled;
  }

  private void Move_canceled(InputAction.CallbackContext obj)
  {
    SetButtonColor(obj.ReadValue<Vector2>());
  }

  private void Move_performed(InputAction.CallbackContext obj)
  {
    SetButtonColor(obj.ReadValue<Vector2>());
  }

  private void SetButtonColor(Vector2 movementVector)
  {
    if (movementVector.y > 0)
      upButtonImage.color = pressedColor;
    else if (movementVector.y < 0)
      downButtonImage.color = pressedColor;
    else
      upButtonImage.color = downButtonImage.color = normalColor;
    
    if (movementVector.x > 0)
      rightButtonImage.color = pressedColor;
    else if (movementVector.x < 0)
      leftButtonImage.color = pressedColor;
    else
      leftButtonImage.color = rightButtonImage.color = normalColor;
  }
}
