using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
  [SerializeField] private Character player;
  [SerializeField] private CharacterBox playerBox;
  [SerializeField] private Sprite playerSprite;
  [SerializeField] private Sprite sleepSprite; 
  
  private SpriteRenderer spriteRenderer;
  private bool isInBox = true;
  private void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = null;

    playerBox.OnEnter += (_, _) =>
    {
      spriteRenderer.sprite = null;
      isInBox = true;
    };
    playerBox.OnExit += (_, _) =>
    {
      spriteRenderer.sprite = playerSprite;
      isInBox = false;
    };

    player.OnReachGoal += (sender, args) => { }; //play animation
    player.OnExitGoal += (sender, args) => { }; // play animation
    
    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch;
  }

  private void GameInput_OnSwitch(object sender, OnSwitchCharacterEventArgs e)
  {
    if (CharacterManager.NumCharacter == 1 || isInBox || (player && player.HasReachGoal())) return;
    spriteRenderer.sprite = player.GetCharacterType() != e.Character ? sleepSprite : playerSprite;
  }
}
