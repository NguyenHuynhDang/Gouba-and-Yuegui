using UnityEngine.SceneManagement;

public static class SceneLoader
{
  private static Scene targetScene;

  public static void Load(Scene scene)
  {
    SceneLoader.targetScene = scene;

    SceneManager.LoadScene(Scene.Loading.ToString());
  }

  public static void LoaderCallBack()
  {
    SceneManager.LoadScene(targetScene.ToString());
  }
}