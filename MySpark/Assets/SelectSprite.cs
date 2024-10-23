using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSprite : MonoBehaviour
{
    // �� ���� ��������Ʈ ĳ���� ������ ������ ��������Ʈ �迭
    public Sprite[] sprite1Levels = new Sprite[4];  // sprite1�� ������ ��������Ʈ
    public Sprite[] sprite2Levels = new Sprite[4];  // sprite2�� ������ ��������Ʈ

    private int currentLevel;  // ���� ����

    private void Start()
    {
        // ���� ������ ExperienceManager���� �ҷ����� (�Ǵ� PlayerPrefs���� �ҷ�����)
        currentLevel = PlayerPrefs.GetInt("����", 1) - 1; // ������ 1���� �����ϹǷ� �ε����� ���߱� ���� -1
        currentLevel = Mathf.Clamp(currentLevel, 0, 3);  // ������ 1~4�� �°� Ŭ����
    }

    // sprite1 ���� �� ������ �´� ��������Ʈ ����
    public void SelectSprite1()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", currentLevel);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[currentLevel].name);  // ���õ� ��������Ʈ �̸� ����
        SceneManager.LoadScene("Main/Main");
    }

    // sprite2 ���� �� ������ �´� ��������Ʈ ����
    public void SelectSprite2()
    {
        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", currentLevel);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[currentLevel].name);  // ���õ� ��������Ʈ �̸� ����
        SceneManager.LoadScene("Main/Main");
    }
}
