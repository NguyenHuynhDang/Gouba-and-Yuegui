using System;
using UnityEngine;

public class OnMoveEventArgs : EventArgs
{
  public Vector2 MovementVector;
}

public class OnSwitchCharacterEventArgs : EventArgs
{
  public Character Character;
}

public class OnNextLevelEventArgs : EventArgs
{
  public Scene NextLevel;
}

