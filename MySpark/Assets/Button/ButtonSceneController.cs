using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToButtonScene : MonoBehaviour
{
    public void GoToButtonScene()
    {
        SceneManager.LoadScene("Button/1");
    }
    public void GoToButtonScene2()
    {
        SceneManager.LoadScene("Button/2");
    }
    public void GoToButtonScene3()
    {
        SceneManager.LoadScene("Button/5");
    }
}
