using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneController : MonoBehaviour
{
    public Image characterImage; // Button/1 씬의 이미지
    public Sprite sprite1;       // 스프라이트 1
    public Sprite sprite2;       // 스프라이트 2

    void Start()
    {
        // PlayerPrefs에서 선택된 스프라이트 정보를 가져와서 이미지에 표시
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");

        if (selectedSprite == "Sprite1")
        {
            characterImage.sprite = sprite1;
        }
        else if (selectedSprite == "Sprite2")
        {
            characterImage.sprite = sprite2;
        }
    }
}
