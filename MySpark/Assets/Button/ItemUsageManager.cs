using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUsageManager : MonoBehaviour
{
    public TextMeshProUGUI[] itemCountTexts; // 각 아이템 소지 갯수 표시 텍스트
    public Button[] itemButtons; // 1번부터 4번까지의 버튼
    public int[] itemCounts = new int[4]; // 각 아이템의 소지 갯수 배열 (1번 버튼부터 4번 버튼까지)

    private void Start()
    {
        // 각 버튼의 클릭 이벤트에 대응하는 스탯 변화 함수 연결
        itemButtons[0].onClick.AddListener(() => UseWisdomItem(0));  // 1번 버튼: 지혜로움 +1
        itemButtons[1].onClick.AddListener(() => UseItem(1, -2, 0)); // 2번 버튼: 허기 -2
        itemButtons[2].onClick.AddListener(() => UseItem(2, -5, 0)); // 3번 버튼: 허기 -5
        itemButtons[3].onClick.AddListener(() => UseItem(3, -8, 0)); // 4번 버튼: 허기 -8

        // UI 초기화
        UpdateItemUI();
    }

    // 1번 버튼의 아이템을 사용하고 지혜로움 +1을 추가하는 함수
    private void UseWisdomItem(int itemIndex)
    {
        if (itemCounts[itemIndex] <= 0)
        {
            // 아이템이 없으면 아무 동작도 하지 않음
            return;
        }

        // 지혜로움 스탯 +1
        StatManager.Instance.지혜로움 = Mathf.Clamp(StatManager.Instance.지혜로움 + 1, 0, 10);

        // 아이템 사용 후 소지 갯수 차감
        itemCounts[itemIndex]--;

        // Eat Eff 사운드 재생
        SoundManager.instance.PlaySpecialEffect("EatButton");

        // 스탯 저장 및 UI 업데이트
        StatManager.Instance.SaveStatsToPlayerPrefs();
        UpdateItemUI();
    }

    // 다른 아이템을 사용하고 스탯을 조정하는 함수
    private void UseItem(int itemIndex, int hungerChange, int interestChange)
    {
        if (itemCounts[itemIndex] <= 0)
        {
            // 아이템이 없으면 아무 동작도 하지 않음
            return;
        }

        // 허기 및 관심도 조정
        StatManager.Instance.허기 = Mathf.Clamp(StatManager.Instance.허기 + hungerChange, 0, 10);
        StatManager.Instance.관심도 = Mathf.Clamp(StatManager.Instance.관심도 + interestChange, 0, 15);

        // 아이템 사용 후 소지 갯수 차감
        itemCounts[itemIndex]--;

        // Eat Eff 사운드 재생
        SoundManager.instance.PlaySpecialEffect("EatButton");

        // 스탯 저장 및 UI 업데이트
        StatManager.Instance.SaveStatsToPlayerPrefs();
        UpdateItemUI();
    }

    // 아이템 갯수 및 버튼 상태 업데이트
    public void UpdateItemUI()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (itemCounts[i] > 0)
            {
                EnableButton(itemButtons[i]);
            }
            else
            {
                DisableButton(itemButtons[i]);
            }

            // 아이템 갯수 텍스트 업데이트
            itemCountTexts[i].text = "x" + itemCounts[i].ToString();
        }
    }

    // 버튼 활성화 함수
    private void EnableButton(Button button)
    {
        button.interactable = true;
        var colors = button.colors;
        colors.normalColor = Color.white; // 활성화 시 색상 설정 (기본 흰색)
        button.colors = colors;
    }

    // 버튼 비활성화 함수
    private void DisableButton(Button button)
    {
        button.interactable = false;
        var colors = button.colors;
        colors.disabledColor = new Color(0.7f, 0.7f, 0.7f); // 비활성화 시 회색으로 설정
        button.colors = colors;
    }
}
