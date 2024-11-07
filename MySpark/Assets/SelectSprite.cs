using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSprite : MonoBehaviour
{
    public Sprite[] sprite1Levels = new Sprite[4];
    public Sprite[] sprite2Levels = new Sprite[4];

    private int currentDay;
    private int selectedSpriteIndex;
    private bool canSelect = false; // 초기화 시 선택 불가능 상태

    private void Start()
    {
        // PlayerPrefs에서 현재 날짜 불러오기 (기본값은 1)
        currentDay = PlayerPrefs.GetInt("현재날짜", 1);

        // 날짜에 따라 스프라이트 인덱스 설정 (1~2일은 0, 3~4일은 1, 5~6일은 2, 7일은 3)
        if (currentDay <= 2)
            selectedSpriteIndex = 0;
        else if (currentDay <= 4)
            selectedSpriteIndex = 1;
        else if (currentDay <= 6)
            selectedSpriteIndex = 2;
        else
            selectedSpriteIndex = 3;

        // 0.5초 후 선택 가능하도록 설정
        Invoke("EnableSelection", 0.5f);
    }

    private void EnableSelection()
    {
        canSelect = true; // 선택 가능 상태로 변경
    }

    public void SelectSprite1()
    {
        if (!canSelect) return; // 선택 불가능할 경우 함수 종료

        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[selectedSpriteIndex].name);
        SceneManager.LoadScene("Main/Main");
    }

    public void SelectSprite2()
    {
        if (!canSelect) return; // 선택 불가능할 경우 함수 종료

        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[selectedSpriteIndex].name);
        SceneManager.LoadScene("Main/Main");
    }
}
