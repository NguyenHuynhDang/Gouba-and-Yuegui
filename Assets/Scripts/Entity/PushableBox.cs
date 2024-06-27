using System.Collections;
using UnityEngine;

public class PushableBox : MovableObject
{
  public IEnumerator PushBox(Vector3 direction)
  {
    yield return MoveObject(direction);
  }

  public bool CanPush(Vector3 direction)
  {
    GameObject collideObject = GetCollideObject(direction);
    
    if (HasObstacleTile(direction) || IsObstacle(collideObject))
    {
      return false;
    }

    return true;
  }
  private bool IsObstacle(GameObject gameObj)
  {
    if (!gameObj) // is null
      return false;
    
    if (gameObj.GetComponent<Key>())
      return false;
    
    return true;
  }

}