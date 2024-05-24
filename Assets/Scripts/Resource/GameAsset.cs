using UnityEngine;

public class GameAsset : MonoBehaviour
{
  public static GameAsset Instance { get; private set; }
    
  [SerializeField] private Path path;
  [SerializeField] private Path pathL;
  
  private void Awake()
  {
    Instance = this;
  }

  public Path GetPath()
  {
    return path;
  }

  public Path GetPathL()
  {
    return pathL;
  }
}
