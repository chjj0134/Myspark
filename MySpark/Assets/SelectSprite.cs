using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSprite : MonoBehaviour
{
    public Sprite[] sprite1Levels = new Sprite[4];
    public Sprite[] sprite2Levels = new Sprite[4];

    private int currentDay;
    private int selectedSpriteIndex;
    private bool canSelect = false; // �ʱ�ȭ �� ���� �Ұ��� ����

    private void Start()
    {
        // PlayerPrefs���� ���� ��¥ �ҷ����� (�⺻���� 1)
        currentDay = PlayerPrefs.GetInt("���糯¥", 1);

        // ��¥�� ���� ��������Ʈ �ε��� ���� (1~2���� 0, 3~4���� 1, 5~6���� 2, 7���� 3)
        if (currentDay <= 2)
            selectedSpriteIndex = 0;
        else if (currentDay <= 4)
            selectedSpriteIndex = 1;
        else if (currentDay <= 6)
            selectedSpriteIndex = 2;
        else
            selectedSpriteIndex = 3;

        // 0.5�� �� ���� �����ϵ��� ����
        Invoke("EnableSelection", 0.5f);
    }

    private void EnableSelection()
    {
        canSelect = true; // ���� ���� ���·� ����
    }

    public void SelectSprite1()
    {
        if (!canSelect) return; // ���� �Ұ����� ��� �Լ� ����

        PlayerPrefs.SetString("SelectedSprite", "Sprite1");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite1Levels[selectedSpriteIndex].name);
        SceneManager.LoadScene("Main/Main");
    }

    public void SelectSprite2()
    {
        if (!canSelect) return; // ���� �Ұ����� ��� �Լ� ����

        PlayerPrefs.SetString("SelectedSprite", "Sprite2");
        PlayerPrefs.SetInt("SelectedSpriteLevel", selectedSpriteIndex);
        PlayerPrefs.SetString("SelectedSpriteImage", sprite2Levels[selectedSpriteIndex].name);
        SceneManager.LoadScene("Main/Main");
    }
}
