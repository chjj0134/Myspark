using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchSceneChange : MonoBehaviour
{
    public string sceneName = "Select/Select";  // 전환할 씬 이름
    private bool canChangeScene = true;         // 씬 전환 가능 여부
    public float delayBeforeNextTouch = 2f;     // 씬 전환 후 터치 입력을 막는 딜레이 시간

    void Update()
    {
        // 씬 전환이 가능하고, 터치 입력이 있을 때만 씬을 전환
        if (canChangeScene && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // 씬 전환
            SceneManager.LoadScene(sceneName);
            // 터치 입력을 일정 시간 동안 무시
            canChangeScene = false;
            Invoke("EnableSceneChange", delayBeforeNextTouch);  // 일정 시간 후 다시 터치 입력을 받음
        }
    }

    // 씬 전환 가능하게 다시 설정하는 함수
    void EnableSceneChange()
    {
        canChangeScene = true;
    }
}
