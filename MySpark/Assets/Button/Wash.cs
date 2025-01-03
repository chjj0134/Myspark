using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Wash : MonoBehaviour
{
    public Button washButton1;
    public Button washButton2;
    public GameObject popupPanel;
    public GameObject blackBackground;
    public TextMeshProUGUI resultText;

    public SpriteRenderer playerSpriteRenderer;
    public Sprite[] player1Sprites = new Sprite[4];
    public Sprite[] player2Sprites = new Sprite[4];

    public int fatigueReduction = -2;
    public int interestIncrease = 4;

    private int currentLevel;
    private Sprite originalSprite;
    private string selectedSprite;
    private bool isWashButton1Used = false; // washButton1 사용 여부
    private bool isWashButton2Used = false; // washButton2 사용 여부

    private void Start()
    {
        popupPanel.SetActive(false);
        DisableButton(washButton2);

        selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        currentLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 1) - 1;
        currentLevel = Mathf.Clamp(currentLevel, 0, player1Sprites.Length - 1);

        // 날짜에 따른 스프라이트 초기화
        int dayLevel = GetSpriteLevelByDay();
        playerSpriteRenderer.sprite = selectedSprite == "Sprite1"
            ? player1Sprites[dayLevel]
            : player2Sprites[dayLevel];
        originalSprite = playerSpriteRenderer.sprite; // originalSprite를 현재 레벨에 맞게 설정
        SaveDefaultSprite();

        washButton1.onClick.AddListener(() =>
        {
            if (!isWashButton1Used)
            {
                ActivateWashButton2WithDelay();
                isWashButton1Used = true;
            }
        });

        washButton2.onClick.AddListener(() =>
        {
            if (!isWashButton2Used)
            {
                StartCoroutine(ShowPopupAndAdjustStats());
                isWashButton2Used = true;
            }
        });
    }


    private void SaveDefaultSprite()
    {
        PlayerPrefs.SetString("DefaultSprite", selectedSprite);
        PlayerPrefs.SetInt("DefaultSpriteLevel", currentLevel + 1);
    }

    private void ActivateWashButton2WithDelay()
    {
        SoundManager.instance.PlaySpecialEffect("ShowerButton");

        // 현재 날짜 기준으로 레벨 가져오기
        int dayLevel = GetSpriteLevelByDay();

        // 캐릭터 스프라이트 설정
        playerSpriteRenderer.sprite = selectedSprite == "Sprite1" ? player1Sprites[dayLevel] : player2Sprites[dayLevel];

        float showerEffDuration = SoundManager.instance.showerEff.length;
        StartCoroutine(ResetSpriteAfterDelay(showerEffDuration));
        DisableButton(washButton1);
        StartCoroutine(EnableButtonAfterDelay(washButton2, showerEffDuration));
    }

    private int GetSpriteLevelByDay()
    {
        return StatManager.Instance.GetSpriteLevelByDay();
    }

    private IEnumerator ShowPopupAndAdjustStats()
    {
        SoundManager.instance.PlaySpecialEffect("BubbleButton");

        if (StatManager.Instance != null)
        {
            StatManager.Instance.피로도 = Mathf.Clamp(StatManager.Instance.피로도 + fatigueReduction, 0, 10);
            StatManager.Instance.관심도 = Mathf.Clamp(StatManager.Instance.관심도 + interestIncrease, 0, 10);
            StatManager.Instance.SaveStatsToPlayerPrefs();
        }

        yield return new WaitForSeconds(SoundManager.instance.bubbleEff.length);

        popupPanel.SetActive(true);
        blackBackground.SetActive(true);
        resultText.text = selectedSprite == "Sprite1" ? "시원하다옹!" : "시원하다멍!";
    }

    public void ResetWashButtonUsage()
    {
        isWashButton1Used = false;
        isWashButton2Used = false;
        EnableButton(washButton1);
        DisableButton(washButton2);
    }

    private IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset할 때 레벨에 맞는 원래 스프라이트 적용
        int dayLevel = GetSpriteLevelByDay();
        originalSprite = selectedSprite == "Sprite1"
            ? player1Sprites[dayLevel]
            : player2Sprites[dayLevel]; // 레벨에 맞는 스프라이트로 갱신
        playerSpriteRenderer.sprite = originalSprite;
    }


    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(button);
    }

    private void EnableButton(Button button)
    {
        button.interactable = true;
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;
    }

    private void DisableButton(Button button)
    {
        button.interactable = false;
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f);
        button.colors = colors;
    }
}
