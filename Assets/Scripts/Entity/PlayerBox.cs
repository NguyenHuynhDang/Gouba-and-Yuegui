using System;
using UnityEngine;

public class PlayerBox : PushableBox
{
  public event EventHandler OnEnter;
  public event EventHandler OnExit;

  public bool IsEmpty { get; private set; }

  private void Start()
  {
    IsEmpty = false;
  }
  
  private void OnTriggerEnter2D(Collider2D col)
  {
    OnEnter?.Invoke(this, EventArgs.Empty);
    IsEmpty = false;
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    OnExit?.Invoke(this, EventArgs.Empty);
    IsEmpty = true;
  }
}
