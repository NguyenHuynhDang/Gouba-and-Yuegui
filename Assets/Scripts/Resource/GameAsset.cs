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

  public Path getPath()
  {
    return path;
  }

  public Path getPathL()
  {
    return pathL;
  }
}
