using UnityEngine;

public class CharacterBoxVisual : MonoBehaviour
{ 
  [SerializeField] private Sprite emptyBoxSprite;
  [SerializeField] private Sprite fullBoxSprite;
  [SerializeField] private CharacterBox characterBox;
 
  private SpriteRenderer spriteRenderer;
  
  private void Start()
  {
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = fullBoxSprite; // default

    characterBox.OnEnter += (_, _) => spriteRenderer.sprite = fullBoxSprite;
    characterBox.OnExit += (_, _) => spriteRenderer.sprite = emptyBoxSprite;
  }
}
