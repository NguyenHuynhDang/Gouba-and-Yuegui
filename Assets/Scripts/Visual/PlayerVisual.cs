using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
  [SerializeField] private Player player;
  [SerializeField] private PlayerBox playerBox;
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
    if (PlayerManager.NumPlayer == 1 || isInBox || (player && player.HasReachGoal())) return;
    spriteRenderer.sprite = player.GetCharacter() != e.Character ? sleepSprite : playerSprite;
  }
}
