using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    public Button backButton;  // 메인으로 돌아가는 버튼
    public GameObject resultPopup;  // 결과 팝업 오브젝트
    public GameObject blackOverlay;  // Black 오버레이 오브젝트

    void Start()
    {
        // 기존 버튼 클릭 이벤트는 유지
        backButton.onClick.AddListener(LoadMainScene);
    }

    // "Main/Main" 씬을 로드하는 기존 함수
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main/Main");
    }

    // 결과 팝업과 오버레이를 비활성화하고 메인 씬으로 돌아가는 새로운 함수
    public void ClosePopupAndReturnToMain()
    {
        // 결과 팝업과 Black 오브젝트가 활성화되어 있을 때만 비활성화
        if (resultPopup != null && resultPopup.activeSelf)
        {
            resultPopup.SetActive(false);  // 결과 팝업 비활성화
        }

        if (blackOverlay != null && blackOverlay.activeSelf)
        {
            blackOverlay.SetActive(false);  // Black 오버레이 비활성화
        }

        // "Main/Main" 씬 로드
        SceneManager.LoadScene("Main/Main");
    }
}
