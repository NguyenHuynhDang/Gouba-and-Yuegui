using System;
using UnityEngine;

public class Lock : MonoBehaviour
{
  [SerializeField] private Key keyObject;
  private void Start()
  {
    keyObject.OnPickUp += KeyObject_OnPickUp;
  }

  private void KeyObject_OnPickUp(object sender, EventArgs e)
  {
    Destroy(gameObject);
  }
}
