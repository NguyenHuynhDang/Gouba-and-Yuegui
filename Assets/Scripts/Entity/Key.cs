using System;
using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
  public event EventHandler OnPickUp;
  
  [SerializeField] private Lock lockObject;
  
  private void OnTriggerEnter2D(Collider2D col)
  {
    StartCoroutine(MoveObject());
  }
  
  private IEnumerator MoveObject()
  {
    float elapsedTime = 0f;
    float timeToMove = 0.5f;
        
    Vector3 originalPosition = transform.position;
    Vector3 targetPosition = lockObject.transform.position;

    while (elapsedTime < timeToMove)
    {
      transform.position = Vector3.Lerp(originalPosition,
        targetPosition, elapsedTime / timeToMove);
      elapsedTime += Time.deltaTime;
      yield return null;
    }
    
    OnPickUp?.Invoke(this, EventArgs.Empty);
    Destroy(gameObject);
  }
  
}
