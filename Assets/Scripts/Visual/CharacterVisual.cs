using System;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
  [SerializeField] private Character character;
  [SerializeField] private CharacterBox characterBox;
  [SerializeField] private Sprite idleSprite;
  [SerializeField] private Sprite sleepSprite; 
  
  private SpriteRenderer spriteRenderer;
  private Animator animator;
  private bool isInBox = true;
  private void Start()
  {
    animator = gameObject.GetComponent<Animator>();
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    characterBox.OnEnter += (_, _) => { isInBox = true; };
    characterBox.OnExit += (_, _) => { isInBox = false; };
    
    character.OnMove += Character_OnMove;
    character.OnStopMoving += Character_OnStopMoving;
    
    character.OnPush += Character_OnPush;
    character.OnStopPushing += Character_OnStopPushing;

    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch;
  }

  private void Character_OnStopPushing(object sender, EventArgs e)
  {
    animator.SetBool("IsPushing", false);
  }

  private void Character_OnPush(object sender, EventArgs e)
  {
    animator.SetBool("IsPushing", true);
  }

  private void Character_OnStopMoving(object sender, EventArgs e)
  {
    animator.SetBool("IsMoving", false);
    animator.SetBool("IsInBox", isInBox);
  }

  private void Character_OnMove(object sender, OnMoveEventArgs e)
  {
    animator.SetFloat("Horizontal", e.MovementVector.x);
    animator.SetFloat("Vertical", e.MovementVector.y);
    animator.SetBool("IsMoving", true);
  }

  private void GameInput_OnSwitch(object sender, OnSwitchCharacterEventArgs e)
  {
    if (CharacterManager.NumCharacter == 1 || isInBox || (character && character.HasReachGoal())) return;
    spriteRenderer.sprite = character.GetCharacterType() != e.Character ? sleepSprite : idleSprite;
  }
}
