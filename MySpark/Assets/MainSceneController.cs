using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    public Image characterImage; // Button/1 ���� �̹���

    // �� ĳ������ ������ ��������Ʈ �迭
    public Sprite[] sprite1Levels = new Sprite[4]; // sprite1�� ������ ��������Ʈ �迭
    public Sprite[] sprite2Levels = new Sprite[4]; // sprite2�� ������ ��������Ʈ �迭

    void Start()
    {
        // PlayerPrefs���� ���õ� ��������Ʈ�� ���� ������ ������
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        int selectedSpriteLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 0); // �⺻�� 0

        // ���õ� ĳ���Ϳ� ������ ���� �̹����� ��������Ʈ�� ǥ��
        if (selectedSprite == "Sprite1")
        {
            characterImage.sprite = sprite1Levels[selectedSpriteLevel];
        }
        else if (selectedSprite == "Sprite2")
        {
            characterImage.sprite = sprite2Levels[selectedSpriteLevel];
        }
    }
}
