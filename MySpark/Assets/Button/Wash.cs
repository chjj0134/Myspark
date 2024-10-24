using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wash : MonoBehaviour
{
    public Button washButton1; // �Ƿε� ���� ��ư1
    public Button washButton2; // �Ƿε� ���� ��ư2
    public GameObject popupPanel; // �˾�â ������Ʈ
    public GameObject blackBackground; // ���� ��� ������Ʈ
    public TextMeshProUGUI resultText; // ��� �ؽ�Ʈ (�˾�â�� ǥ�õ� �ؽ�Ʈ)

    public SpriteRenderer playerSpriteRenderer; // �÷��̾��� SpriteRenderer

    // �� �÷��̾��� ������ ��������Ʈ �迭 (���õ� �÷��̾ �°� ����)
    public Sprite[] player1Sprites = new Sprite[4]; // Player 1�� ������ ��������Ʈ
    public Sprite[] player2Sprites = new Sprite[4]; // Player 2�� ������ ��������Ʈ

    public int fatigueReduction = -2; // �Ƿε� ���ҷ�
    public int interestIncrease = 4; // ���ɵ� ������

    private int currentLevel; // ���� ���� (0~3)
    private Sprite originalSprite; // �÷��̾��� ���� ��������Ʈ
    private string selectedSprite; // ���õ� ��������Ʈ

    private void Start()
    {
        // �˾�â�� ó���� ��Ȱ��ȭ ���·� ����
        popupPanel.SetActive(false);

        // ó���� ��ư2�� ��Ȱ��ȭ ���·� ����
        DisableButton(washButton2);

        // PlayerPrefs���� ���õ� ��������Ʈ�� ���� ������ ������
        selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        currentLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 1) - 1; // ������ 1���� �����ϹǷ� -1

        // currentLevel ���� �迭 ������ ���� �ʵ��� ����
        currentLevel = Mathf.Clamp(currentLevel, 0, player1Sprites.Length - 1);

        // ���� ��������Ʈ ����
        if (selectedSprite == "Sprite1")
        {
            playerSpriteRenderer.sprite = player1Sprites[currentLevel]; // Player 1�� ��������Ʈ ����
        }
        else if (selectedSprite == "Sprite2")
        {
            playerSpriteRenderer.sprite = player2Sprites[currentLevel]; // Player 2�� ��������Ʈ ����
        }

        // ���� ��������Ʈ ����
        originalSprite = playerSpriteRenderer.sprite;

        // ��ư Ŭ�� �̺�Ʈ ����
        washButton1.onClick.AddListener(ActivateWashButton2WithDelay);
        washButton2.onClick.AddListener(() => StartCoroutine(ShowPopupAndAdjustStats()));
    }

    // washButton1 Ŭ�� �� ���� SE ��� �� ��������Ʈ ���� �� ��ư2 Ȱ��ȭ
    private void ActivateWashButton2WithDelay()
    {
        // Shower Eff ���� ���
        SoundManager.instance.PlaySpecialEffect("ShowerButton");

        // ��������Ʈ ����
        if (selectedSprite == "Sprite1")
        {
            playerSpriteRenderer.sprite = player1Sprites[currentLevel];
        }
        else if (selectedSprite == "Sprite2")
        {
            playerSpriteRenderer.sprite = player2Sprites[currentLevel];
        }

        // SE�� ���� �� ��������Ʈ ������� ����
        float showerEffDuration = SoundManager.instance.showerEff.length;
        StartCoroutine(ResetSpriteAfterDelay(showerEffDuration));

        // ��ư1 ��Ȱ��ȭ, ��ư2 Ȱ��ȭ
        DisableButton(washButton1);
        StartCoroutine(EnableButtonAfterDelay(washButton2, showerEffDuration));
    }

    // washButton2 Ŭ�� �� �Ƿε� ����, ���ɵ� ���, SE ��� �� �˾�â ǥ��
    private IEnumerator ShowPopupAndAdjustStats()
    {
        // Bubble Eff ���� ���
        SoundManager.instance.PlaySpecialEffect("BubbleButton");

        // �Ƿε� ���� �� ���ɵ� ���
        if (StatManager.Instance != null)
        {
            StatManager.Instance.�Ƿε� = Mathf.Clamp(StatManager.Instance.�Ƿε� + fatigueReduction, 0, 10); // �Ƿε� 2 ����
            StatManager.Instance.���ɵ� = Mathf.Clamp(StatManager.Instance.���ɵ� + interestIncrease, 0, 10); // ���ɵ� 4 ����
            StatManager.Instance.SaveStatsToPlayerPrefs(); // ���� ����
        }
        else
        {
            Debug.LogError("StatManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }

        // SE ��� �ð��� ���� ������ ���
        float bubbleEffDuration = SoundManager.instance.bubbleEff.length;
        yield return new WaitForSeconds(bubbleEffDuration);

        // �˾�â�� ���� ��� Ȱ��ȭ
        popupPanel.SetActive(true);
        blackBackground.SetActive(true);

        // ��������Ʈ�� �´� ��� �޽��� ����
        if (selectedSprite == "Sprite1")
        {
            resultText.text = "�ÿ��ϴٿ�!";
        }
        else if (selectedSprite == "Sprite2")
        {
            resultText.text = "�ÿ��ϴٸ�!";
        }
    }

    // ���� �ð� �� ��������Ʈ�� ������� �����ϴ� �ڷ�ƾ
    private IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerSpriteRenderer.sprite = originalSprite; // ��������Ʈ ����
    }

    // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ
    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(button);
    }

    // ��ư Ȱ��ȭ �Լ�
    private void EnableButton(Button button)
    {
        button.interactable = true;
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;
    }

    // ��ư ��Ȱ��ȭ �Լ�
    private void DisableButton(Button button)
    {
        button.interactable = false;
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f);
        button.colors = colors;
    }
}
