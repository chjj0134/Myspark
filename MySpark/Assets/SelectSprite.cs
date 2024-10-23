using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSprite : MonoBehaviour
{
    // 두 가지 스프라이트 캐릭터 각각의 레벨별 스프라이트 배열
    public Sprite[] sprite1Levels = new Sprite[4];  // sprite1의 레벨별 스프라이트
    public Sprite[] sprite2Levels = new Sprite[4];  // sprite2의 레벨별 스프라이트

    private int currentLevel;  // 현재 레벨

    private void Start()
    {
        // 현재 레벨을 ExperienceManager에서 불러오기 (또는 PlayerPrefs에서 불러오기)
        currentLevel = PlayerPrefs.GetInt("레벨", 1) - 1; // 레벨은 1부터 시작하므로 인덱스를 맞추기 위해 -1
        currentLevel = Mathf.Clamp(currentLevel, 0, 3);  // 레벨이 1~4에 맞게 클램프
    }

    // sprite1 선택 시 레벨에 맞는 스프라이트 설정
    public void SelectSprite1()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", currentLevel);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[currentLevel].name);  // 선택된 스프라이트 이름 저장
        SceneManager.LoadScene("Main/Main");
    }

    // sprite2 선택 시 레벨에 맞는 스프라이트 설정
    public void SelectSprite2()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", currentLevel);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[currentLevel].name);  // 선택된 스프라이트 이름 저장
        SceneManager.LoadScene("Main/Main");
    }
}
