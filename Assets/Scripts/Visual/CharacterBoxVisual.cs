using UnityEngine;

public class PlayerBoxVisual : MonoBehaviour
{ 
  [SerializeField] private Sprite emptyBoxSprite;
  [SerializeField] private Sprite fullBoxSprite;
  [SerializeField] private CharacterBox playerBox;
 
  private SpriteRenderer spriteRenderer;
  
  private void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = fullBoxSprite; // default

    playerBox.OnEnter += (_, _) => spriteRenderer.sprite = fullBoxSprite;
    playerBox.OnExit += (_, _) => spriteRenderer.sprite = emptyBoxSprite;
  }
}
