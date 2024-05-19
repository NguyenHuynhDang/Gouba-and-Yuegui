using UnityEngine.SceneManagement;

public static class SceneLoader
{
  private static Scene targetScene;

  public static void Load(Scene targetScene)
  {
    SceneLoader.targetScene = targetScene;

    SceneManager.LoadScene(Scene.Loading.ToString());
  }

  public static void LoaderCallBack()
  {
    SceneManager.LoadScene(targetScene.ToString());
  }
}