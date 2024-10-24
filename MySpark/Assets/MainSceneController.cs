using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer; // 캐릭터의 SpriteRenderer

    // 두 캐릭터의 레벨별 스프라이트 배열
    public Sprite[] sprite1Levels = new Sprite[4]; // sprite1의 레벨별 스프라이트 배열
    public Sprite[] sprite2Levels = new Sprite[4]; // sprite2의 레벨별 스프라이트 배열

    void Start()
    {
        // PlayerPrefs에서 선택된 스프라이트와 레벨 정보를 가져옴
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        int selectedSpriteLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 0); // 기본값 0

        // 선택된 캐릭터와 레벨에 따라 SpriteRenderer의 스프라이트를 설정
        if (selectedSprite == "Sprite1")
        {
            characterSpriteRenderer.sprite = sprite1Levels[selectedSpriteLevel];
        }
        else if (selectedSprite == "Sprite2")
        {
            characterSpriteRenderer.sprite = sprite2Levels[selectedSpriteLevel];
        }
    }
}
