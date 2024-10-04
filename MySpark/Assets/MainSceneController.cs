using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneController : MonoBehaviour
{
    public Image characterImage; // Button/1 ���� �̹���
    public Sprite sprite1;       // ��������Ʈ 1
    public Sprite sprite2;       // ��������Ʈ 2

    void Start()
    {
        // PlayerPrefs���� ���õ� ��������Ʈ ������ �����ͼ� �̹����� ǥ��
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
