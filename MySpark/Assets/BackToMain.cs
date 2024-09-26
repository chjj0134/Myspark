using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    // 버튼을 Unity 에디터에서 할당할 수 있도록 공개
    public Button backButton;

    void Start()
    {
        // 버튼에 클릭 이벤트 추가
        backButton.onClick.AddListener(LoadMainScene);
    }

    // "Main/Main" 씬을 로드하는 함수
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main/Main");
    }
}
