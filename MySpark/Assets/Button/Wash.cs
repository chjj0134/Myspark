using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Wash : MonoBehaviour
{
    public Button washButton1; // 피로도 감소 버튼1
    public Button washButton2; // 피로도 감소 버튼2
    public GameObject popupPanel; // 팝업창 오브젝트
    public GameObject blackBackground; // 검은 배경 오브젝트
    public TextMeshProUGUI resultText; // 결과 텍스트 (팝업창에 표시될 텍스트)

    public SpriteRenderer playerSpriteRenderer; // 플레이어의 SpriteRenderer

    // 두 플레이어의 레벨별 스프라이트 배열 (선택된 플레이어에 맞게 변경)
    public Sprite[] player1Sprites = new Sprite[4]; // Player 1의 레벨별 스프라이트
    public Sprite[] player2Sprites = new Sprite[4]; // Player 2의 레벨별 스프라이트

    public int fatigueReduction = -2; // 피로도 감소량
    public int interestIncrease = 4; // 관심도 증가량

    private int currentLevel; // 현재 레벨 (0~3)
    private Sprite originalSprite; // 플레이어의 원래 스프라이트
    private string selectedSprite; // 선택된 스프라이트

    private void Start()
    {
        // 팝업창은 처음에 비활성화 상태로 설정
        popupPanel.SetActive(false);

        // 처음에 버튼2는 비활성화 상태로 설정
        DisableButton(washButton2);

        // PlayerPrefs에서 선택된 스프라이트와 레벨 정보를 가져옴
        selectedSprite = PlayerPrefs.GetString("SelectedSprite");
        currentLevel = PlayerPrefs.GetInt("SelectedSpriteLevel", 1) - 1; // 레벨은 1부터 시작하므로 -1

        // currentLevel 값이 배열 범위를 넘지 않도록 제한
        currentLevel = Mathf.Clamp(currentLevel, 0, player1Sprites.Length - 1);

        // 원래 스프라이트 저장
        if (selectedSprite == "Sprite1")
        {
            playerSpriteRenderer.sprite = player1Sprites[currentLevel]; // Player 1의 스프라이트 설정
        }
        else if (selectedSprite == "Sprite2")
        {
            playerSpriteRenderer.sprite = player2Sprites[currentLevel]; // Player 2의 스프라이트 설정
        }

        // 원래 스프라이트 저장
        originalSprite = playerSpriteRenderer.sprite;

        // 버튼 클릭 이벤트 연결
        washButton1.onClick.AddListener(ActivateWashButton2WithDelay);
        washButton2.onClick.AddListener(() => StartCoroutine(ShowPopupAndAdjustStats()));
    }

    // washButton1 클릭 시 샤워 SE 재생 후 스프라이트 변경 및 버튼2 활성화
    private void ActivateWashButton2WithDelay()
    {
        // Shower Eff 사운드 재생
        SoundManager.instance.PlaySpecialEffect("ShowerButton");

        // 스프라이트 변경
        if (selectedSprite == "Sprite1")
        {
            playerSpriteRenderer.sprite = player1Sprites[currentLevel];
        }
        else if (selectedSprite == "Sprite2")
        {
            playerSpriteRenderer.sprite = player2Sprites[currentLevel];
        }

        // SE가 끝난 후 스프라이트 원래대로 복구
        float showerEffDuration = SoundManager.instance.showerEff.length;
        StartCoroutine(ResetSpriteAfterDelay(showerEffDuration));

        // 버튼1 비활성화, 버튼2 활성화
        DisableButton(washButton1);
        StartCoroutine(EnableButtonAfterDelay(washButton2, showerEffDuration));
    }

    // washButton2 클릭 시 피로도 감소, 관심도 상승, SE 재생 후 팝업창 표시
    private IEnumerator ShowPopupAndAdjustStats()
    {
        // Bubble Eff 사운드 재생
        SoundManager.instance.PlaySpecialEffect("BubbleButton");

        // 피로도 감소 및 관심도 상승
        if (StatManager.Instance != null)
        {
            StatManager.Instance.피로도 = Mathf.Clamp(StatManager.Instance.피로도 + fatigueReduction, 0, 10); // 피로도 2 감소
            StatManager.Instance.관심도 = Mathf.Clamp(StatManager.Instance.관심도 + interestIncrease, 0, 10); // 관심도 4 증가
            StatManager.Instance.SaveStatsToPlayerPrefs(); // 스탯 저장
        }
        else
        {
            Debug.LogError("StatManager 인스턴스를 찾을 수 없습니다.");
        }

        // SE 재생 시간이 끝날 때까지 대기
        float bubbleEffDuration = SoundManager.instance.bubbleEff.length;
        yield return new WaitForSeconds(bubbleEffDuration);

        // 팝업창과 검은 배경 활성화
        popupPanel.SetActive(true);
        blackBackground.SetActive(true);

        // 스프라이트에 맞는 결과 메시지 설정
        if (selectedSprite == "Sprite1")
        {
            resultText.text = "시원하다옹!";
        }
        else if (selectedSprite == "Sprite2")
        {
            resultText.text = "시원하다멍!";
        }
    }

    // 일정 시간 후 스프라이트를 원래대로 복구하는 코루틴
    private IEnumerator ResetSpriteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerSpriteRenderer.sprite = originalSprite; // 스프라이트 복구
    }

    // 일정 시간 후 버튼을 활성화하는 코루틴
    private IEnumerator EnableButtonAfterDelay(Button button, float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(button);
    }

    // 버튼 활성화 함수
    private void EnableButton(Button button)
    {
        button.interactable = true;
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;
    }

    // 버튼 비활성화 함수
    private void DisableButton(Button button)
    {
        button.interactable = false;
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f);
        button.colors = colors;
    }
}
