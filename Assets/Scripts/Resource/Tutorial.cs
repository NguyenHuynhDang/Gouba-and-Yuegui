using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
  [SerializeField] private List<GameObject> tutorials;

  public GameObject GetTutorial(int index)
  {
    return tutorials[index];
  }

  public int Size()
  {
    return tutorials.Count;
  }
}
