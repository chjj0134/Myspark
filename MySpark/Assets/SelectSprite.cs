using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSprite : MonoBehaviour
{
    // 두 가지 스프라이트를 위한 변수
    public Sprite sprite1;
    public Sprite sprite2;

    // 버튼 클릭 이벤트
    public void SelectSprite1()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        SceneManager.LoadScene("Main/Main");
    }

    public void SelectSprite2()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        SceneManager.LoadScene("Main/Main");
    }
}
