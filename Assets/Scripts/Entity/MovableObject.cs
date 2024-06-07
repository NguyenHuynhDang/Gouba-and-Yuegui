using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovableObject : MonoBehaviour
{
  [SerializeField] protected Tilemap groundTilemap;
  [SerializeField] protected Tilemap obstacleTilemap;

  protected bool IsMoving;
  private const float TimeToMove = 0.3f;
    
  protected IEnumerator MoveObject(Vector3 direction)
  {
    IsMoving = true;
    float elapsedTime = 0f;
        
    Vector3 originalPosition = transform.position;
    Vector3 targetPosition = originalPosition + direction;

    while (elapsedTime < TimeToMove)
    {
      transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / TimeToMove);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    transform.position = targetPosition;
    IsMoving = false;
  }

  protected bool HasObstacleTile(Vector3 direction)
  {
    Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + direction);
        
    if (!groundTilemap.HasTile(gridPosition) || obstacleTilemap.HasTile(gridPosition))
    {
      return true;
    }
    
    return false;
  }

  protected List<GameObject> GetCollideObjects(Vector3 direction)
  { 
    List<RaycastHit2D> raycastHit2D = new List<RaycastHit2D>();
    Physics2D.Raycast(transform.position + direction, direction, new ContactFilter2D().NoFilter(), raycastHit2D, 0.01f);
    
    List<GameObject> output = new List<GameObject>();
    foreach (var raycast in raycastHit2D)
    {
      output.Add(raycast.collider.gameObject);
    }

    return output;
  }
  
  protected GameObject GetCollideObject(Vector3 direction)
  {
    RaycastHit2D hit = Physics2D.Raycast(transform.position+direction, direction, 0.01f, 1);
    if (hit)
    {
      return hit.collider.gameObject;
    }

    return null;
    
    List<RaycastHit2D> raycastHit2D = new List<RaycastHit2D>();
    int size = Physics2D.Raycast(transform.position + direction, direction, new ContactFilter2D().NoFilter(), raycastHit2D, 0.01f);
    
    if (size > 1)
    {
      foreach (var raycast in raycastHit2D)
      {
        MovableObject movableObject = raycast.collider.gameObject.GetComponent<MovableObject>();
        if (!movableObject || movableObject is not Character) continue;
        raycastHit2D.Remove(raycast);
        break;
      }
    }
    
    if (size > 0)
    {
      return raycastHit2D[0].collider.gameObject;
    }
    
    return null;
  }
}
