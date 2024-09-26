using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // 버튼에 할당할 씬 이름을 배열로 저장
    public string[] sceneNames = { "Button/1", "Button/2", "Button/4", "Button/5" };

    // 각각의 UI 버튼을 할당
    public Button button1;
    public Button button2;
    public Button button4;
    public Button button5;

    void Start()
    {
        // 버튼에 클릭 이벤트 연결
        button1.onClick.AddListener(() => LoadScene(0)); // Button/1 씬으로 이동
        button2.onClick.AddListener(() => LoadScene(1)); // Button/2 씬으로 이동
        button4.onClick.AddListener(() => LoadScene(2)); // Button/4 씬으로 이동
        button5.onClick.AddListener(() => LoadScene(3)); // Button/5 씬으로 이동
    }

    // 씬을 로드하는 메소드
    void LoadScene(int index)
    {
        if (index >= 0 && index < sceneNames.Length)
        {
            SceneManager.LoadScene(sceneNames[index]);
        }
        else
        {
            Debug.LogError("잘못된 씬 인덱스입니다.");
        }
    }
}
