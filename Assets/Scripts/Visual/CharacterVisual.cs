using UnityEngine;
using UnityEngine.Serialization;

public class CharacterVisual : MonoBehaviour
{
  [SerializeField] private Character character;
  [SerializeField] private CharacterBox characterBox;
  [SerializeField] private Sprite idleSprite;
  [SerializeField] private Sprite sleepSprite; 
  
  private SpriteRenderer spriteRenderer;
  private bool isInBox = true;
  private void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = null;

    characterBox.OnEnter += (_, _) =>
    {
      spriteRenderer.sprite = null;
      isInBox = true;
    };
    characterBox.OnExit += (_, _) =>
    {
      spriteRenderer.sprite = idleSprite;
      isInBox = false;
    };

    character.OnReachGoal += (sender, args) => { }; //play animation
    character.OnExitGoal += (sender, args) => { }; // play animation
    
    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch;
  }

  private void GameInput_OnSwitch(object sender, OnSwitchCharacterEventArgs e)
  {
    if (CharacterManager.NumCharacter == 1 || isInBox || (character && character.HasReachGoal())) return;
    spriteRenderer.sprite = character.GetCharacterType() != e.Character ? sleepSprite : idleSprite;
  }
}
