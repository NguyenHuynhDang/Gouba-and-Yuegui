using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MovableObject
{
  public event EventHandler OnReachGoal;
  public event EventHandler OnExitGoal;

  [SerializeField] private CharacterType characterType;
  [SerializeField] private Goal goal;
  [SerializeField] private int moveLimit;

  private Direction firstDirection = Direction.None;
  private Stack<Vector3> moveStack;
  private Stack<Path> pathVisualStack;
  private BoxCollider2D boxCollider2D;
  private bool hasReachGoal;
  
  private void Start()
  {
    moveStack = new Stack<Vector3>();
    pathVisualStack = new Stack<Path>();
    boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
  }
  
  public void MoveCharacter(Vector3 direction)
  {
    if (IsMoving) return;
    GameObject collideObject = GetCollideObject(direction);
    
    if (!HasObstacleTile(direction) && !IsObstacle(collideObject))
    {
      PushableBox pushableBox = collideObject ? collideObject.GetComponent<PushableBox>() : null;
      
      if (CanMove(direction, pushableBox))
      {
        StartCoroutine(Move(direction));
      }
    }
  }

  public int GetRemainMove()
  {
    return moveLimit - moveStack.Count;
  }

  public CharacterType GetCharacterType()
  {
    return characterType;
  }

  public bool HasReachGoal()
  {
    return hasReachGoal;
  }

  private IEnumerator Move(Vector3 direction)
  {
    yield return StartCoroutine(MoveObject(direction));
    OnGoal();
  }
  
  private void OnGoal()
  {
    if (transform.position == goal.transform.position)
    {
      hasReachGoal = true;
      goal.OnEnter();
      boxCollider2D.enabled = false;
      OnReachGoal?.Invoke(this, EventArgs.Empty);
    }
    else if (hasReachGoal)
    {
      hasReachGoal = false;
      goal.OnExit();
      boxCollider2D.enabled = true;
      OnExitGoal?.Invoke(this, EventArgs.Empty);
    }
  }
  
  private bool IsObstacle(GameObject gameObj)
  {
    if (!gameObj)
      return false;
    
    if (gameObj.GetComponent<Key>())
      return false;
    
    if (gameObj.GetComponent<PushableBox>())
      return false;

    if (gameObj.GetComponent<CharacterBox>())
      return false;

    if (gameObj.GetComponent<Goal>())
      return false;

    Path path = gameObj.GetComponent<Path>();
    if (path && pathVisualStack.Peek() == path)
    {
      return false;
    }

    return true;
  }
  private bool CanMove(Vector3 moveDirection, PushableBox pushableBox)
  {
    Vector3 currentPosition = transform.position;
    if (moveStack.Count > 0 && currentPosition + moveDirection == moveStack.Peek()) // going back
    {
      moveStack.Pop();
      DestroyPathVisual();
      return true;
    }

    if (moveStack.Count < moveLimit) // going forward
    {
      if (pushableBox)
      {
        if (pushableBox is CharacterBox {IsEmpty: true} || !pushableBox.CanPush(moveDirection))
        {
          return false;
        }
        pushableBox.PushBox(moveDirection);
      }

      moveStack.Push(currentPosition);
      CreatePathVisual(moveDirection);
      return true;
    } 
    
    return false;
  }
  
  private void CreatePathVisual(Vector3 moveDirection)
  {
    Direction curDirection = ToDirection(moveDirection);

    if (moveStack.Count > 1)
    {
      if (pathVisualStack.Count == 0)
      {
        CreatePathVisual(firstDirection, curDirection);
      }
      else
      {
        CreatePathVisual(pathVisualStack.Peek().Direction, curDirection);
      }
    }
    else
    {
      firstDirection = curDirection;
    }
  }

  private void CreatePathVisual(Direction preDir, Direction curDir)
  {
    Path path = null;
    if (preDir == curDir)
    {
      if (curDir == Direction.Left || curDir == Direction.Right)
      {
        path = Instantiate(GameAsset.Instance.GetPath(), transform.position, Quaternion.identity);
      }
      else if (curDir == Direction.Up || curDir == Direction.Down)
      {
        path = Instantiate(GameAsset.Instance.GetPath(), transform.position, Quaternion.Euler(0, 0, 90));
      }
    }
    else
    {
      switch (preDir)
      {
        case Direction.Down when curDir == Direction.Right:
        case Direction.Left when curDir == Direction.Up:
          path = Instantiate(GameAsset.Instance.GetPathL(), transform.position, Quaternion.identity);
          break;
        case Direction.Down when curDir == Direction.Left:
        case Direction.Right when curDir == Direction.Up:
          path = Instantiate(GameAsset.Instance.GetPathL(), transform.position, Quaternion.Euler(0, 0, 90));
          break;
        case Direction.Up when curDir == Direction.Right:
        case Direction.Left when curDir == Direction.Down:
          path = Instantiate(GameAsset.Instance.GetPathL(), transform.position, Quaternion.Euler(0, 0, -90));
          break;
        case Direction.Up when curDir == Direction.Left:
        case Direction.Right when curDir == Direction.Down:
          path = Instantiate(GameAsset.Instance.GetPathL(), transform.position, Quaternion.Euler(0, 0, 180));
          break;
      }
    }
    
    if (path)
    {
      path.Direction = curDir;
      pathVisualStack.Push(path);
    }
  }
  
  private void DestroyPathVisual()
  {
    if (pathVisualStack.Count == 0) return;
    
    Destroy(pathVisualStack.Peek().gameObject);
    pathVisualStack.Pop();
  }

  private Direction ToDirection(Vector3 direction)
  {
    if (direction.x != 0 && direction.y == 0)
    {
      return direction.x > 0 ? Direction.Right : Direction.Left;
    }

    if (direction.y != 0 && direction.x == 0)
    {
      return direction.y > 0 ? Direction.Up : Direction.Down;
    }

    return Direction.None;
  }
}
