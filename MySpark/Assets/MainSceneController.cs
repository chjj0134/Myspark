using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer; // ĳ������ SpriteRenderer

    // �� ĳ������ ������ ��������Ʈ �迭
    public Sprite[] sprite1Levels = new Sprite[4]; // sprite1�� ������ ��������Ʈ �迭
    public Sprite[] sprite2Levels = new Sprite[4]; // sprite2�� ������ ��������Ʈ �迭

    void Start()
    {
        // PlayerPrefs���� ���õ� ��������Ʈ�� ���� ������ ������
        string selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        int selectedSpriteLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 0); // �⺻�� 0

        // ���õ� ĳ���Ϳ� ������ ���� SpriteRenderer�� ��������Ʈ�� ����
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
