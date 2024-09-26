using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneChange : MonoBehaviour
{
    // 전환할 씬의 이름을 입력
    public string sceneName= "Select/Select";

    void Update()
    {
        // 화면에 터치 입력이 있으면 씬을 전환
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 씬 전환
            SceneManager.LoadScene(sceneName);
        }
    }
}
