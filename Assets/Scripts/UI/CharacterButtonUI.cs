using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterButtonUI : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI remainStepText;
  [SerializeField] private Character character;
  
  private Vector3 enableScale = new(2, 2, 2);
  private Vector3 disableScale = new(1.5f, 1.5f, 1.5f);
  private float scaleDuration = 0.1f;

  private void Start()
  {
    GameInput.Instance.OnSwitch1 += GameInput_OnSwitch;
    GameInput.Instance.OnSwitch2 += GameInput_OnSwitch;
  }

  private void GameInput_OnSwitch(object sender, OnSwitchCharacterEventArgs e)
  {
    if (PlayerManager.NumPlayer == 1) return;
    ScaleToTarget(e.Character == character ? enableScale : disableScale, scaleDuration);
  }

  private void ScaleToTarget(Vector3 targetScale, float duration)
  {
    StartCoroutine(ScaleToTargetCoroutine(targetScale, duration));
  }
  
  private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration)
  {
    Vector3 startScale = transform.localScale;
    float timer = 0.0f;

    while(timer < duration && startScale != targetScale)
    {
      timer += Time.deltaTime;
      float t = timer / duration;
      //smoother step algorithm
      //t = t * t * t * (t * (6f * t - 15f) + 10f);
      transform.localScale = Vector3.Lerp(startScale, targetScale, t);
      yield return null;
    }
  }

  public void SetRemainStepText(int remainStep)
  {
    remainStepText.text = remainStep.ToString();
  }
}
